import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandlerFn,
    HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/core/auth/auth.service';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { Observable, catchError, throwError } from 'rxjs';

/**
 * Intercept
 *
 * @param req
 * @param next
 */
export const authInterceptor = (
    req: HttpRequest<unknown>,
    next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
    const authService = inject(AuthService);
    const router = inject(Router);

    // Clone the request object
    let newReq = req.clone();

    // Only attach Authorization header to internal backend API requests.
    // External 3rd-party services (OSRM, OpenStreetMap, Leaflet, Nominatim, etc.)
    // will fail with CORS errors if an Authorization header is sent.
    const isAbsolute = req.url.startsWith('http://') || req.url.startsWith('https://');
    const isExternal = isAbsolute &&
        !req.url.includes('germanywestcentral-01.azurewebsites.net') &&
        !req.url.includes('/cm/') &&
        !req.url.includes('/api/');

    if (
        !isExternal &&
        authService.accessToken &&
        !AuthUtils.isTokenExpired(authService.accessToken)
    ) {
        newReq = req.clone({
            headers: req.headers.set(
                'Authorization',
                'Bearer ' + authService.accessToken
            ),
        });
    }

    // Response
    return next(newReq).pipe(
        catchError((error) => {
            // Catch "401 Unauthorized" responses ONLY from internal API routes
            if (!isExternal && error instanceof HttpErrorResponse && error.status === 401) {
                // Sign out
                authService.signOut();
                // Redirect to sign-in without hard refresh to avoid reload loops
                if (!router.url.startsWith('/sign-in')) {
                    void router.navigateByUrl('/sign-in');
                }
            }

            return throwError(error);
        })
    );
};
