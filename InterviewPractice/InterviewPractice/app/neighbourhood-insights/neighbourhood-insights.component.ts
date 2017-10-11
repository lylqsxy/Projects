
import { Component } from '@angular/core';

const neigbInsights = {
    'underTwPer': 23,
    'twToTnPer': 28,
    'FtToFnPer': 25,
    'overStPer': 24,
    'longTermResiPer': 68,
    'ownerPer': 75,
    'familyPer': 60
};

@Component({
    selector: 'neighbourhood-insights',
    templateUrl: './neighbourhood-insights.component.html',
    styleUrls: [`./neighbourhood-insights.component.css`]
})
export class NeighbourhoodInsights {

    neigbInsights = neigbInsights;
}
