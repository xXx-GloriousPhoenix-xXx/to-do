import { Component, inject, output, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormInputComponent } from "../../../common-ui/form-input/form-input.component";
import { TodoService } from '../../../data/services/todo.service';

@Component({
    selector: 'app-create-todo',
    imports: [ReactiveFormsModule, FormInputComponent],
    templateUrl: './create-todo.component.html',
    styleUrl: './create-todo.component.css',
})
export class CreateTodoComponent {
    private todoService = inject(TodoService);

    created = output<void>();
    isLoading = signal(false);

    form = new FormGroup({
        title: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        description: new FormControl('', { nonNullable: true }),
        category: new FormControl('Default', { nonNullable: true }),
        completeUntil: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    });

    onSubmit() {
        if (this.form.invalid) return;

        const raw = this.form.getRawValue();
        this.isLoading.set(true);

        const category = raw.category.trim() || 'Default';

        this.todoService.create({
            title: raw.title,
            description: raw.description,
            category: category,
            completeUntil: new Date(raw.completeUntil).toISOString(),
        }).subscribe({
            next: () => {
                this.isLoading.set(false);
                this.created.emit();
            },
            error: (err) => {
                console.error(err);
                this.isLoading.set(false);
            }
        });
    }
}
