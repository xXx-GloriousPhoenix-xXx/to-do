import { Component } from '@angular/core';
import { SvgIconComponent } from "../../../common-ui/svg-icon/svg-icon.component";
import { RouterLink } from "@angular/router";

@Component({
    selector: 'app-todo-placeholder',
    imports: [SvgIconComponent, RouterLink],
    templateUrl: './todo-placeholder.component.html'
})
export class TodoPlaceholderComponent {}
