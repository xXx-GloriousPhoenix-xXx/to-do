import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { SvgIconComponent } from "../../svg-icon/svg-icon.component";

@Component({
    selector: 'app-workspace-outlet',
    imports: [RouterOutlet, SvgIconComponent, RouterLinkWithHref],
    templateUrl: './workspace-outlet.component.html',
    styleUrl: './workspace-outlet.component.css',
})
export class WorkspaceOutletComponent {}
