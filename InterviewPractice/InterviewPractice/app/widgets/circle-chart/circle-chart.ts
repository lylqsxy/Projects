
import { Component, Input } from "@angular/core";

@Component({
    selector: 'circle-chart',
    templateUrl: `./circle-chart.html`,
    styleUrls: [`./circle-chart.css`]
})
export class CircleChart {
    @Input('percentage')
    number: number;

    @Input('title')
    title: string;

}