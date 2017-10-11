
import {Component, Input} from "@angular/core";
import {NgModule} from "@angular/core";
import {platformBrowserDynamic} from "@angular/platform-browser-dynamic";
import {BrowserModule} from "@angular/platform-browser";



@Component({
    selector: 'app',
    template: `
 
        <search-box placeholder="fff"></search-box>
                
        `
})
export class App {


}

@Component({
    selector: 'search-box',
    template: `
 
        <input placeholder="{{text}}">
                
        `
})
export class SearchBox {
    @Input('placeholder')
    text = 'fdfs';
}


@NgModule({
    declarations: [App, SearchBox],
    imports: [BrowserModule],
    bootstrap: [App]
})
export class AppModule {

}

platformBrowserDynamic().bootstrapModule(AppModule);


