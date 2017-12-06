"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var subInsights = {
    'medPri': '$5.5m',
    'aucCle': '67%',
    'sold': '59',
    'avgDays': '69',
    'population': '12,863',
    'avgAge': '60+',
    'ownerPer': 64,
    'familyPer': 47
};
var SuburbInsights = (function () {
    function SuburbInsights() {
        this.subInsights = subInsights;
    }
    return SuburbInsights;
}());
SuburbInsights = __decorate([
    core_1.Component({
        selector: 'suburb-insights',
        templateUrl: "./suburb-insights.component.html",
        styleUrls: ["./suburb-insights.component.css"]
    })
], SuburbInsights);
exports.SuburbInsights = SuburbInsights;
//# sourceMappingURL=suburb-insights.component.js.map