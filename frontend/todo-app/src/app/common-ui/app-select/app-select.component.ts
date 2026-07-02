import { Component, input, model } from '@angular/core';
import { SelectOption } from '../../data/interfaces/common/select-option.interface';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-select',
    imports: [FormsModule],
    templateUrl: './app-select.component.html'
})
export class SelectComponent<T extends string | number | boolean | null> {
    label = input<string>('');
    options = input.required<SelectOption<T>[]>();

    value = model.required<T>();

    onSelectChange(val: T) {
        this.value.set(val);
    }
}
