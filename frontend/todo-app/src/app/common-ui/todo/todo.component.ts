import { Component, EventEmitter, input, Input, output, Output } from '@angular/core';
import { TodoGetResponse } from '../../data/interfaces/todo/todo-get-response.interface';
import { TodoDatePipe } from '../todo-date.pipe';
import { RouterLink } from '@angular/router';
import { SvgIconComponent } from "../svg-icon/svg-icon.component";

@Component({
    selector: 'app-todo',
    imports: [TodoDatePipe, RouterLink, SvgIconComponent],
    templateUrl: './todo.component.html',
    styleUrl: './todo.component.css',
})
export class TodoComponent {
    todo = input.required<TodoGetResponse>();
    deleted = output<string>();

    onDelete() {
        this.deleted.emit(this.todo().id);
    }
}
