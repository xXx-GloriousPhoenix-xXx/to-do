import { Component, computed, effect, inject, signal, untracked } from '@angular/core';
import { SvgIconComponent } from "../../common-ui/svg-icon/svg-icon.component";
import { TodoService } from '../../data/services/todo.service';
import { TodoGetResponse } from '../../data/interfaces/todo/todo-get-response.interface';
import { TodoComponent } from "../../common-ui/todo/todo.component";
import { FormsModule } from '@angular/forms';
import { TodoSortField } from '../../data/interfaces/todo/todo-sort-field';
import { SortDirection } from '../../data/interfaces/common/sort-direction';
import { TodoFilter } from '../../data/interfaces/todo/todo-filter.interface';
import { TodoSorter } from '../../data/interfaces/todo/todo-sorter.interface';
import { TodoFilterComponent } from "./todo-filter/todo-filter.component";
import { TodoPlaceholderComponent } from "./todo-placeholder/todo-placeholder.component";

@Component({
    selector: 'app-workspace-page',
    imports: [SvgIconComponent, TodoComponent, FormsModule, TodoFilterComponent, TodoPlaceholderComponent],
    templateUrl: './workspace-page.component.html',
    styleUrl: './workspace-page.component.css',
})
export class WorkspacePageComponent {
    private todoService = inject(TodoService);

    isLoading = signal<boolean>(false);

    todos = signal<TodoGetResponse[]>([]);
    currentPage = signal<number>(1);
    pageSize = signal<number>(5);
    pageCount = signal<number>(1);
    totalCount = signal<number>(0);
    hasNext = signal<boolean>(false);
    hasPrevious = signal<boolean>(false);

    categories = signal<string[]>([]);

    selectedCategory = signal<string | null>(null);
    isCompleted = signal<boolean | null>(null);
    completeUntilFrom = signal<string | null>(null);
    completeUntilTo = signal<string | null>(null);
    sortField = signal<TodoSortField>(TodoSortField.CreatedAt);
    sortDirection = signal<SortDirection>(SortDirection.Descending);

    readonly TodoSortField = TodoSortField;
    readonly SortDirection = SortDirection;

    constructor() {
        effect(() => {
            const page = this.currentPage();
            const size = this.pageSize();
            const category = this.selectedCategory();
            const isCompleted = this.isCompleted();
            const from = this.completeUntilFrom();
            const to = this.completeUntilTo();
            const field = this.sortField();
            const direction = this.sortDirection();

            untracked(() => this.loadTodos(page, size, {
                filter: { category, isCompleted, completeUntilFrom: from, completeUntilTo: to },
                sorter: { field, direction }
            }));
        });

        this.todoService.getCategories().subscribe({
            next: (cats) => this.categories.set(cats),
            error: (err) => console.error(err)
        });
    }

    private loadTodos(page: number, size: number, params: {
        filter?: TodoFilter | null,
        sorter?: TodoSorter | null
    } = {}) {
        this.isLoading.set(true);
        this.todoService.getAll(page, size, params.filter, params.sorter).subscribe({
            next: (response) => {
                this.todos.set(response.items);
                this.pageCount.set(response.pageCount);
                this.totalCount.set(response.totalCount);
                this.hasNext.set(response.hasNext);
                this.hasPrevious.set(response.hasPrevious);
                this.isLoading.set(false);
            },
            error: (err) => {
                console.error(err);
                this.isLoading.set(false);
            }
        });
    }

    hasPagination = computed(() => this.pageCount() > 1);

    onNext() {
        if (this.hasNext())
            this.currentPage.update(p => p + 1);
    }

    onPrevious() {
        if (this.hasPrevious())
            this.currentPage.update(p => p - 1);
    }

    selectCategory(category: string | null) {
        this.selectedCategory.set(category);
        this.currentPage.set(1);
    }

    onFilterChange() {
        this.currentPage.set(1);
    }

    resetFilters() {
        this.selectedCategory.set(null);
        this.isCompleted.set(null);
        this.completeUntilFrom.set(null);
        this.completeUntilTo.set(null);
        this.sortField.set(TodoSortField.CreatedAt);
        this.sortDirection.set(SortDirection.Descending);
        this.currentPage.set(1);
    }

    deleteTodo(id: string) {
        this.todoService.delete(id).subscribe({
            next: () => {
                if (this.todos().length === 1 && this.currentPage() > 1) {
                    this.currentPage.update(p => p - 1);
                } else {
                    this.loadTodos(this.currentPage(), this.pageSize(), {
                        filter: {
                            category: this.selectedCategory(),
                            isCompleted: this.isCompleted(),
                            completeUntilFrom: this.completeUntilFrom(),
                            completeUntilTo: this.completeUntilTo()
                        },
                        sorter: {
                            field: this.sortField(),
                            direction: this.sortDirection()
                        }
                    });
                }
            },
            error: (err) => console.error("Failed to delete todo:", err)
        });
    }
}
