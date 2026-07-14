import { Component, ElementRef, OnDestroy, OnInit, QueryList, ViewChildren, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseAlertType } from '@fuse/components/alert';
import { AuthService } from 'app/core/auth/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'auth-verify-code',
    templateUrl: './verify-code.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
    standalone: true,
    imports: [RouterLink, FormsModule, MatProgressSpinnerModule],
})
export class AuthVerifyCodeComponent implements OnInit, OnDestroy {
    @ViewChildren('digitInput') digitInputs: QueryList<ElementRef<HTMLInputElement>>;

    email: string = '';
    /** True juste après l'inscription : l'admin n'a pas encore approuvé, aucun code envoyé. */
    pendingApproval: boolean = false;
    digits: string[] = ['', '', '', '', '', ''];
    isLoading: boolean = false;
    showAlert: boolean = false;
    alert: { type: FuseAlertType; message: string } = { type: 'success', message: '' };

    resendCooldown: number = 0;
    private resendTimer: any;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _authService: AuthService,
        private _router: Router
    ) {}

    ngOnInit(): void {
        this.email = this._activatedRoute.snapshot.queryParamMap.get('email') ?? '';
        this.pendingApproval = this._activatedRoute.snapshot.queryParamMap.get('pending') === '1';

        // Sans email, impossible de vérifier : retour à l'inscription.
        if (!this.email) {
            this._router.navigate(['/sign-up']);
            return;
        }

        // Un code vient d'être envoyé (hors attente d'approbation) : cooldown initial du renvoi.
        if (!this.pendingApproval) {
            this.startResendCooldown();
        }
    }

    ngOnDestroy(): void {
        if (this.resendTimer) {
            clearInterval(this.resendTimer);
        }
    }

    get code(): string {
        return this.digits.join('');
    }

    onDigitInput(index: number, event: Event): void {
        const input = event.target as HTMLInputElement;
        const value = input.value.replace(/\D/g, '');

        if (!value) {
            this.digits[index] = '';
            return;
        }

        // Collage ou saisie multiple : répartir les chiffres.
        const chars = value.split('');
        for (let i = 0; i < chars.length && index + i < 6; i++) {
            this.digits[index + i] = chars[i];
        }
        input.value = this.digits[index];

        const nextIndex = Math.min(index + chars.length, 5);
        this.focusDigit(nextIndex);

        if (this.code.length === 6) {
            this.verify();
        }
    }

    onDigitKeydown(index: number, event: KeyboardEvent): void {
        if (event.key === 'Backspace' && !this.digits[index] && index > 0) {
            this.digits[index - 1] = '';
            this.focusDigit(index - 1);
            event.preventDefault();
        }
        if (event.key === 'ArrowLeft' && index > 0) {
            this.focusDigit(index - 1);
        }
        if (event.key === 'ArrowRight' && index < 5) {
            this.focusDigit(index + 1);
        }
    }

    onPaste(event: ClipboardEvent): void {
        event.preventDefault();
        const pasted = (event.clipboardData?.getData('text') ?? '').replace(/\D/g, '').slice(0, 6);
        if (!pasted) {
            return;
        }
        for (let i = 0; i < 6; i++) {
            this.digits[i] = pasted[i] ?? '';
        }
        this.syncInputs();
        this.focusDigit(Math.min(pasted.length, 5));
        if (this.code.length === 6) {
            this.verify();
        }
    }

    verify(): void {
        if (this.code.length !== 6 || this.isLoading) {
            return;
        }

        this.isLoading = true;
        this.showAlert = false;

        this._authService.verifyEmail(this.email, this.code).subscribe({
            next: (response) => {
                this.isLoading = false;
                if (response?.success && response.data?.authentication) {
                    // Session déjà stockée par AuthService : connexion automatique.
                    this._router.navigateByUrl('/signed-in-redirect');
                } else {
                    this.clearCode();
                    this.showErrorAlert(response?.message || 'Code invalide. Veuillez réessayer.');
                }
            },
            error: (error: HttpErrorResponse) => {
                this.isLoading = false;
                this.clearCode();
                this.showErrorAlert(
                    error?.error?.message ||
                        (error.status === 0
                            ? 'Impossible de se connecter au serveur. Vérifiez votre connexion internet.'
                            : 'La vérification a échoué. Veuillez réessayer.')
                );
            },
        });
    }

    resend(): void {
        if (this.resendCooldown > 0 || this.isLoading) {
            return;
        }

        this.showAlert = false;
        this._authService.resendCode(this.email).subscribe({
            next: (response) => {
                if (response?.success) {
                    this.alert = { type: 'success', message: 'Un nouveau code vous a été envoyé par email.' };
                    this.showAlert = true;
                    this.startResendCooldown();
                } else {
                    this.showErrorAlert(response?.message || "Le renvoi du code a échoué.");
                }
            },
            error: (error: HttpErrorResponse) => {
                this.showErrorAlert(error?.error?.message || 'Le renvoi du code a échoué.');
            },
        });
    }

    private startResendCooldown(): void {
        this.resendCooldown = 60;
        if (this.resendTimer) {
            clearInterval(this.resendTimer);
        }
        this.resendTimer = setInterval(() => {
            this.resendCooldown--;
            if (this.resendCooldown <= 0) {
                clearInterval(this.resendTimer);
            }
        }, 1000);
    }

    private clearCode(): void {
        this.digits = ['', '', '', '', '', ''];
        this.syncInputs();
        this.focusDigit(0);
    }

    private syncInputs(): void {
        this.digitInputs?.forEach((ref, i) => (ref.nativeElement.value = this.digits[i]));
    }

    private focusDigit(index: number): void {
        this.digitInputs?.get(index)?.nativeElement.focus();
    }

    private showErrorAlert(message: string): void {
        this.alert = { type: 'error', message };
        this.showAlert = true;
    }
}
