
import { Component, Input } from "@angular/core";

@Component({
    selector: 'label-widget',
    templateUrl: `./label-widget.html`,
    styleUrls: [`./label-widget.css`]
})
export class LabelWidget {
    @Input()
    title: string;

    @Input()
    details: string;
}