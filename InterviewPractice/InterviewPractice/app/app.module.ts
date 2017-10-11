import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { SuburbInsights } from './suburb-insights/suburb-insights.component';
import { NeighbourhoodInsights } from './neighbourhood-insights/neighbourhood-insights.component';
import { LabelWidget } from './widgets/label-widget/label-widget';
import { CircleChart } from './widgets/circle-chart/circle-chart';
import { BarChartEx } from './widgets/bar-chart-ex/bar-chart-ex';
import { BarChart } from './widgets/bar-chart/bar-chart';

@NgModule({
    imports: [BrowserModule],
    declarations: [AppComponent, SuburbInsights, NeighbourhoodInsights, LabelWidget, CircleChart, BarChartEx, BarChart],
    bootstrap:    [AppComponent]
})
export class AppModule { }
