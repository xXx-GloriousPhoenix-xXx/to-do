import { Component, Input } from '@angular/core';

@Component({
    selector: 'svg[icon]',
    standalone: true,
    template: '<svg:use [attr.href]="href"></svg:use>',
    host: {
        '[attr.viewBox]': 'viewBox'
    },
    styles: [`
        :host {
            display: inline-block;
        }
    `]
})
export class SvgIconComponent {
    @Input() icon: string = '';
    @Input() viewBox: string = '0 0 24 24';

    get href() {
        return `/assets/svgs/${this.icon}.svg#${this.icon}`;
    }
}
