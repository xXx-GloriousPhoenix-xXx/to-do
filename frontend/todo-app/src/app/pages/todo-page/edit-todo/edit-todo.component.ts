import { Component, inject, input, output, signal } from '@angular/core';
import { TodoGetResponse } from '../../../data/interfaces/todo/todo-get-response.interface';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TodoService } from '../../../data/services/todo.service';
import { SelectOption } from '../../../data/interfaces/common/select-option.interface';
import { TodoDatePipe } from "../../../common-ui/todo-date.pipe";
import { FormInputComponent } from "../../../common-ui/form-input/form-input.component";
import { RouterLink } from "@angular/router";

@Component({
    selector: 'app-edit-todo',
    imports: [ReactiveFormsModule, TodoDatePipe, FormInputComponent, RouterLink],
    templateUrl: './edit-todo.component.html',
    styleUrl: './edit-todo.component.css',
})
export class EditTodoComponent {
    private todoService = inject(TodoService);

    todoId = input.required<string>();
    deleted = output<void>();

    todo = signal<TodoGetResponse | null>(null);
    isLoading = signal(false);
    isEditing = signal(false);
    isSaving = signal(false);

    form = new FormGroup({
        title: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
        description: new FormControl('', { nonNullable: true }),
        category: new FormControl('Default', { nonNullable: true }),
        completeUntil: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    });

    ngOnInit() {
        this.isLoading.set(true);
        this.todoService.getById(this.todoId()).subscribe({
            next: (todo) => {
                this.todo.set(todo);
                this.fillForm(todo);
                this.isLoading.set(false);
            },
            error: (err) => {
                console.error(err);
                this.isLoading.set(false);
            }
        });
    }

    private fillForm(todo: TodoGetResponse) {
        const date = new Date(todo.completeUntil);
        const local = new Date(date.getTime() - date.getTimezoneOffset() * 60000)
            .toISOString()
            .slice(0, 16);

        this.form.patchValue({
            title: todo.title,
            description: todo.description,
            completeUntil: local,
            category: todo.category || 'Default'
        });
    }

    startEditing() {
        this.isEditing.set(true);
    }

    cancelEditing() {
        const todo = this.todo();
        if (todo) this.fillForm(todo);
        this.isEditing.set(false);
    }

    saveChanges() {
        if (this.form.invalid) return;

        const raw = this.form.getRawValue();
        this.isSaving.set(true);

        const category = raw.category.trim() || 'Default';

        this.todoService.update(this.todoId(), {
            title: raw.title,
            description: raw.description,
            category: category,
            completeUntil: new Date(raw.completeUntil).toISOString(),
        }).subscribe({
            next: () => {
                const updatedTodo = {
                    ...this.todo()!,
                    title: raw.title,
                    description: raw.description,
                    category: category,
                    completeUntil: raw.completeUntil,
                };
    
                this.todo.set(updatedTodo);
                this.fillForm(updatedTodo);
                this.isEditing.set(false);
                this.isSaving.set(false);
            },
            error: (err) => {
                console.error(err);
                this.isSaving.set(false);
            }
        });
    }

    deleteTodo() {
        if (!confirm('Delete this task?')) return;

        this.todoService.delete(this.todoId()).subscribe({
            next: () => this.deleted.emit(),
            error: (err) => console.error(err)
        });
    }
}
