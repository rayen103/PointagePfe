import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Navigation } from 'app/core/navigation/navigation.types';
import { map, Observable, of, ReplaySubject, switchMap, take, tap } from 'rxjs';
import {
    compactNavigation,
    defaultNavigation,
    futuristicNavigation,
    horizontalNavigation,
} from './navigation.data';
import { FuseNavigationItem, FuseNavigationService } from '../../../@fuse/components/navigation';
import { cloneDeep } from 'lodash-es';
import { UserService } from '../user/user.service';
import { RoleNavigation } from '../role-utilisateur/role-utilisateur.model';

@Injectable({ providedIn: 'root' })
export class NavigationService {
    private _userService = inject(UserService);
    private _fuseNavigationService = inject(FuseNavigationService);
    private _navigation: ReplaySubject<Navigation> =
        new ReplaySubject<Navigation>(1);

    private readonly _compactNavigation: FuseNavigationItem[] =
        compactNavigation;
    private readonly _defaultNavigation: FuseNavigationItem[] =
        defaultNavigation;
    private readonly _futuristicNavigation: FuseNavigationItem[] =
        futuristicNavigation;
    private readonly _horizontalNavigation: FuseNavigationItem[] =
        horizontalNavigation;

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for navigation
     */
    get navigation$(): Observable<Navigation> {
        return this._navigation.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get all navigation data
     */
    get(): Observable<Navigation> {
        return this._userService.user$.pipe(
            take(1),
            switchMap((user) => {
                const isSuperAdmin = !user?.navigations || user.navigations.length === 0;
                const allowedSet = new Set(user?.navigations?.map((n) => n.navigationId) ?? []);

                // Always work with fresh deep clones so the base definitions are never mutated
                let defaultNav: FuseNavigationItem[] = cloneDeep(this._defaultNavigation);
                let compactNav: FuseNavigationItem[] = cloneDeep(this._compactNavigation);
                let futuristicNav: FuseNavigationItem[] = cloneDeep(this._futuristicNavigation);
                let horizontalNav: FuseNavigationItem[] = cloneDeep(this._horizontalNavigation);

                if (!isSuperAdmin) {
                    const flatNavigation = this._fuseNavigationService.getFlatNavigation(defaultNav);
                    flatNavigation.forEach((item) => {
                        if (!allowedSet.has(item.id)) {
                            defaultNav = this._fuseNavigationService.removeItemById(defaultNav, item.id);
                        }
                    });
                }

                // Copy filtered children from defaultNav to compactNav
                compactNav.forEach((compactNavItem) => {
                    const defaultNavItem = defaultNav.find((d) => d.id === compactNavItem.id);
                    if (defaultNavItem) {
                        compactNavItem.children = cloneDeep(defaultNavItem.children);
                        compactNavItem.action = cloneDeep(defaultNavItem.action ?? []);
                        compactNavItem.section = cloneDeep(defaultNavItem.section ?? []);
                    }
                });

                // Copy filtered children from defaultNav to futuristicNav
                futuristicNav.forEach((futuristicNavItem) => {
                    const defaultNavItem = defaultNav.find((d) => d.id === futuristicNavItem.id);
                    if (defaultNavItem) {
                        futuristicNavItem.children = cloneDeep(defaultNavItem.children);
                        futuristicNavItem.action = cloneDeep(defaultNavItem.action ?? []);
                        futuristicNavItem.section = cloneDeep(defaultNavItem.section ?? []);
                    }
                });

                // Copy filtered children from defaultNav to horizontalNav
                horizontalNav.forEach((horizontalNavItem) => {
                    const defaultNavItem = defaultNav.find((d) => d.id === horizontalNavItem.id);
                    if (defaultNavItem) {
                        horizontalNavItem.children = cloneDeep(defaultNavItem.children);
                        horizontalNavItem.action = cloneDeep(defaultNavItem.action ?? []);
                        horizontalNavItem.section = cloneDeep(defaultNavItem.section ?? []);
                    }
                });

                // For restricted users, link parent groups to their first permitted child,
                // and hide any parent groups that have 0 accessible children left.
                if (!isSuperAdmin) {
                    const syncAndFilterNav = (items: FuseNavigationItem[]): FuseNavigationItem[] => {
                        return items
                            .map((item) => {
                                if (item.children && item.children.length > 0) {
                                    return {
                                        ...item,
                                        link: item.children[0].link || item.link,
                                    };
                                }
                                return item;
                            })
                            .filter((item) => {
                                if (item.type === 'group' || item.type === 'aside' || item.type === 'collapsable') {
                                    return !!item.children && item.children.length > 0;
                                }
                                return true;
                            });
                    };

                    defaultNav = syncAndFilterNav(defaultNav);
                    compactNav = syncAndFilterNav(compactNav);
                    futuristicNav = syncAndFilterNav(futuristicNav);
                    horizontalNav = syncAndFilterNav(horizontalNav);
                }

                const navigation: Navigation = {
                    compact: compactNav,
                    default: defaultNav,
                    futuristic: futuristicNav,
                    horizontal: horizontalNav,
                };

                this._navigation.next(navigation);

                return of(navigation);
            })
        );
    }
}
