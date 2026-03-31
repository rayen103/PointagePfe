export interface DashboardChange {
    value: number;
    label: string;
    isPositive: boolean;
}

export interface DashboardKpi {
    id: string;
    title: string;
    value: number;
    icon: string;
    color: string;
    change?: DashboardChange;
}

export interface DashboardAxisSeries {
    name: string;
    data: number[];
}

export interface DashboardAxisChart {
    labels: string[];
    series: DashboardAxisSeries[];
}

export interface DashboardPieChart {
    labels: string[];
    series: number[];
}

export interface DashboardCharts {
    bar: DashboardAxisChart;
    line: DashboardAxisChart;
    pie: DashboardPieChart;
    doughnut: DashboardPieChart;
}

export type DashboardActivityType = 'created' | 'updated' | 'system';

export interface DashboardActivityItem {
    id: string;
    title: string;
    description?: string;
    date?: Date;
    icon: string;
    type: DashboardActivityType;
}

export interface DashboardQuickAction {
    id: string;
    title: string;
    description: string;
    icon: string;
    link: string;
}

export interface DashboardData {
    kpis: DashboardKpi[];
    charts: DashboardCharts;
    recentCreated: DashboardActivityItem[];
    recentUpdated: DashboardActivityItem[];
    systemActivity: DashboardActivityItem[];
    lastUpdated: Date;
    errorMessage?: string;
}
