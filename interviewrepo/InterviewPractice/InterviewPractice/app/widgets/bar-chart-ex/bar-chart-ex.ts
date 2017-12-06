
import { Component, Input } from "@angular/core";

@Component({
    selector: 'bar-chart-ex',
    templateUrl: `./bar-chart-ex.html`,
    styleUrls: [`./bar-chart-ex.css`]
})
export class BarChartEx {
    @Input('percentage')
    number: number;

    @Input('title-left')
    titleLeft: string;

    @Input('title-right')
    titleRight: string;

}