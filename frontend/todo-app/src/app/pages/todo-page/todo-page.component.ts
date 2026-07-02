import { Component, inject, signal } from '@angular/core';
import { CreateTodoComponent } from './create-todo/create-todo.component';
import { ActivatedRoute, Router } from '@angular/router';
import { EditTodoComponent } from './edit-todo/edit-todo.component';

@Component({
    selector: 'app-todo-page',
    imports: [CreateTodoComponent, EditTodoComponent],
    templateUrl: './todo-page.component.html',
    styleUrl: './todo-page.component.css',
})
export class TodoPageComponent {
    private route = inject(ActivatedRoute);
    private router = inject(Router);

    todoId = signal<string | null>(this.route.snapshot.paramMap.get('id'));
    isCreateMode = signal<boolean>(!this.todoId());

    onCreated() {
        this.router.navigate(['/workspace']);
    }

    onDeleted() {
        this.router.navigate(['/workspace']);
    }
}
