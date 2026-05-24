import {
    IsActiveMatchOptions,
    Params,
    QueryParamsHandling,
} from '@angular/router';

export interface FuseNavigationItem {
    id?: string;
    title?: string;
    subtitle?: string;
    type: 'aside' | 'basic' | 'collapsable' | 'divider' | 'group' | 'spacer' | 'section';
    hidden?: (item: FuseNavigationItem) => boolean;
    active?: boolean;
    disabled?: boolean;
    tooltip?: string;
    link?: string;
    fragment?: string;
    preserveFragment?: boolean;
    queryParams?: Params | null;
    queryParamsHandling?: QueryParamsHandling | null;
    externalLink?: boolean;
    target?: '_blank' | '_self' | '_parent' | '_top' | string;
    exactMatch?: boolean;
    isActiveMatchOptions?: IsActiveMatchOptions;
    function?: (item: FuseNavigationItem) => void;
    classes?: {
        title?: string;
        subtitle?: string;
        icon?: string;
        wrapper?: string;
    };
    icon?: string;
    badge?: {
        title?: string;
        classes?: string;
    };
    children?: FuseNavigationItem[];
    section?: FuseNavigationItem[];
    meta?: any;
    action?: FuseNavigationAction[];
}

export enum FuseNavigationAction {
    View = 0,
    Add = 1,
    Edit = 2,
    Delete = 3,
    Preview = 4,
    Print = 5,
    Export = 6,
    Search = 7,
    Duplicate = 8,
}

export type FuseVerticalNavigationAppearance =
    | 'default'
    | 'compact'
    | 'dense'
    | 'thin';

export type FuseVerticalNavigationMode = 'over' | 'side';

export type FuseVerticalNavigationPosition = 'left' | 'right';
