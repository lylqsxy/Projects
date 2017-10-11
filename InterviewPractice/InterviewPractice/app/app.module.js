"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var app_component_1 = require("./app.component");
var suburb_insights_component_1 = require("./suburb-insights/suburb-insights.component");
var neighbourhood_insights_component_1 = require("./neighbourhood-insights/neighbourhood-insights.component");
var label_widget_1 = require("./widgets/label-widget/label-widget");
var circle_chart_1 = require("./widgets/circle-chart/circle-chart");
var bar_chart_ex_1 = require("./widgets/bar-chart-ex/bar-chart-ex");
var bar_chart_1 = require("./widgets/bar-chart/bar-chart");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule],
        declarations: [app_component_1.AppComponent, suburb_insights_component_1.SuburbInsights, neighbourhood_insights_component_1.NeighbourhoodInsights, label_widget_1.LabelWidget, circle_chart_1.CircleChart, bar_chart_ex_1.BarChartEx, bar_chart_1.BarChart],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map