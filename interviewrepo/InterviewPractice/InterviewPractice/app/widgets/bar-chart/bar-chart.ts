
import { Component, Input } from "@angular/core";

@Component({
    selector: 'bar-chart',
    templateUrl: `./bar-chart.html`,
    styleUrls: [`./bar-chart.css`]
})
export class BarChart {
    @Input('percentage')
    number: number;

    @Input('title')
    title: string;
}