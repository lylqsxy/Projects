
import { Component } from '@angular/core';

const subInsights = {
    'medPri': '$5.5m',
    'aucCle': '67%',
    'sold': '59',
    'avgDays': '69',
    'population': '12,863',
    'avgAge': '60+',
    'ownerPer': 64,
    'familyPer': 47
};

@Component({
    selector: 'suburb-insights',
    templateUrl: `./suburb-insights.component.html`,
    styleUrls: [`./suburb-insights.component.css`]
})
export class SuburbInsights  {

    subInsights = subInsights;
}
