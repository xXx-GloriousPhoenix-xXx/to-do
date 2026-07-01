import { Component, input, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { SvgIconComponent } from "../svg-icon/svg-icon.component";

@Component({
    selector: 'app-form-input',
    imports: [ReactiveFormsModule, SvgIconComponent],
    templateUrl: './form-input.component.html',
    styleUrl: './form-input.component.css',
})
export class FormInputComponent {
    label = input.required<string>();
    placeholder = input<string>('');
    type = input<'text' | 'password'>('text');
    control = input.required<FormControl<string>>();

    isPasswordHidden = signal(true);
}
