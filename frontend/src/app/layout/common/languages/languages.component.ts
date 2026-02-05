import { NgTemplateOutlet } from '@angular/common';
import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    OnDestroy,
    OnInit,
    ViewEncapsulation,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import {
    FuseNavigationService,
    FuseVerticalNavigationComponent,
} from '@fuse/components/navigation';
import { AvailableLangs, TranslocoService } from '@ngneat/transloco';
import { take } from 'rxjs';

@Component({
    selector: 'languages',
    templateUrl: './languages.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'languages',
    standalone: true,
    imports: [MatButtonModule, MatMenuModule, NgTemplateOutlet],
})
export class LanguagesComponent implements OnInit, OnDestroy {
    availableLangs: AvailableLangs;
    activeLang: string;
    flagCodes: any;

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _fuseNavigationService: FuseNavigationService,
        private _translocoService: TranslocoService
    ) {}

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        // Get the available languages from transloco
        this.availableLangs = this._translocoService.getAvailableLangs();

        // Subscribe to language changes
        this._translocoService.langChanges$.subscribe((activeLang) => {
            // Get the active lang
            this.activeLang = activeLang;

            // Update the navigation
            this._updateNavigation(activeLang);
        });

        // Set the country iso codes for languages for flags
        this.flagCodes = {
            en: 'us',
            tr: 'tr',
            fr:'fr',
            es:'es',
            it:'it'
        };
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {}

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Set the active lang
     *
     * @param lang
     */
    setActiveLang(lang: string): void {
        // Set the active lang
        this._translocoService.setActiveLang(lang);
    }

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any {
        return item.id || index;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Update the navigation
     *
     * @param lang
     * @private
     */
    private _updateNavigation(lang: string): void {
        // For the demonstration purposes, we will only update the Dashboard names
        // from the navigation but you can do a full swap and change the entire
        // navigation data.
        //
        // You can import the data from a file or request it from your backend,
        // it's up to you.

        // Get the component -> navigation data -> item
        const navComponent =
            this._fuseNavigationService.getComponent<FuseVerticalNavigationComponent>(
                'mainNavigation'
            );

        // Return if the navigation component does not exist
        if (!navComponent) {
            return null;
        }

        // Get the flat navigation data
        const navigation = navComponent.navigation;



        //fichier
        const FichierItem = this._fuseNavigationService.getItem(
            'fichier',
            navigation
        );
        if (FichierItem) {
            this._translocoService
                .selectTranslate('File')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    FichierItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }

        //fichier.user
        const FichierUserItem = this._fuseNavigationService.getItem(
            'fichier.utilisateur',
            navigation
        );
        if (FichierUserItem) {
            this._translocoService
                .selectTranslate('User')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    FichierUserItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }

        //fichier.societe
        const SocieteItem = this._fuseNavigationService.getItem(
            'fichier.societe',
            navigation
        );
        if (SocieteItem) {
            this._translocoService
                .selectTranslate('Company')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    SocieteItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }

        //fichier.role
        const RoleItem = this._fuseNavigationService.getItem(
            'fichier.role-utilisateur',
            navigation
        );
        if (RoleItem) {
            this._translocoService
                .selectTranslate('Role')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    RoleItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //dashboard
        const DashboardItem = this._fuseNavigationService.getItem(
            'dashboards',
            navigation
        );
        if (DashboardItem) {
            this._translocoService
                .selectTranslate('Dashboard')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    DashboardItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //dashboard.child
        const DashboardChildItem = this._fuseNavigationService.getItem(
            'dashboards.dashboard',
            navigation
        );
        if (DashboardChildItem) {
            this._translocoService
                .selectTranslate('Dashboard')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    DashboardChildItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //dashboard.ColorItem
        const ColorItem = this._fuseNavigationService.getItem(
            'dashboards.color',
            navigation
        );
        if (ColorItem) {
            this._translocoService
                .selectTranslate('RAL-Color-Reference')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    ColorItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }

        //bonMelange
        const BonMelangeItem = this._fuseNavigationService.getItem(
            'bonMelanges',
            navigation
        );
        if (BonMelangeItem) {
            this._translocoService
                .selectTranslate('Good-Mix')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    BonMelangeItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //bonMelange.Child
        const BonMelangeChildItem = this._fuseNavigationService.getItem(
            'bonMelanges.bonMelange',
            navigation
        );
        if (BonMelangeChildItem) {
            this._translocoService
                .selectTranslate('Good-Mix')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    BonMelangeChildItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }

        //bonMelange.gestionBonMelange
        const GestionBonMelangeItem = this._fuseNavigationService.getItem(
            'bonMelanges.gestionBonMelange',
            navigation
        );
        if (GestionBonMelangeItem) {
            this._translocoService
                .selectTranslate('Good-Mix-Management')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    GestionBonMelangeItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //bonMelange.gestionBonMelangeClient
        const GestionBonMelangeClientItem = this._fuseNavigationService.getItem(
            'bonMelanges.bonMelangeBySociete',
            navigation
        );
        if (GestionBonMelangeClientItem) {
            this._translocoService
                .selectTranslate('Good-Mix-ManagementClient')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    GestionBonMelangeClientItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }


        //configurations
        const ConfigurationItem = this._fuseNavigationService.getItem(
            'configurations',
            navigation
        );
        if (ConfigurationItem) {
            this._translocoService
                .selectTranslate('configurations')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    ConfigurationItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //configurations.base
        const BaseItem = this._fuseNavigationService.getItem(
            'configurations.base',
            navigation
        );
        if (BaseItem) {
            this._translocoService
                .selectTranslate('Base')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    BaseItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }
        //configurations.formule
        const FormuleItem = this._fuseNavigationService.getItem(
            'configurations.formule',
            navigation
        );
        if (FormuleItem) {
            this._translocoService
                .selectTranslate('Formula')
                .pipe(take(1))
                .subscribe((translation) => {
                    // Set the title
                    FormuleItem.title = translation;

                    // Refresh the navigation component
                    navComponent.refresh();
                });
        }

    }
}
