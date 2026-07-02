import { Component, computed, input, model, output, signal } from '@angular/core';
import { TodoSortField } from '../../../data/interfaces/todo/todo-sort-field';
import { SortDirection } from '../../../data/interfaces/common/sort-direction';
import { SelectOption } from '../../../data/interfaces/common/select-option.interface';
import { FormsModule } from '@angular/forms';
import { SelectComponent } from "../../../common-ui/app-select/app-select.component";

@Component({
    selector: 'app-todo-filter',
    imports: [FormsModule, SelectComponent],
    templateUrl: './todo-filter.component.html'
})
export class TodoFilterComponent {
    readonly TodoSortField = TodoSortField;
    readonly SortDirection = SortDirection;

    categories = input<string[]>([]);

    selectedCategory = model<string | null>(null);
    isCompleted = model<boolean | null>(null);
    completeUntilFrom = model<string | null>(null);
    completeUntilTo = model<string | null>(null);
    sortField = model<TodoSortField>(TodoSortField.CreatedAt);
    sortDirection = model<SortDirection>(SortDirection.Descending);

    filterChanged = output<void>();

    categoryOptions = computed<SelectOption<string | null>[]>(() => [
        { label: 'All', value: null },
        ...this.categories().map(cat => ({ label: cat, value: cat }))
    ]);

    statusOptions: SelectOption<boolean | null>[] = [
        { label: 'All', value: null },
        { label: 'Active', value: false },
        { label: 'Completed', value: true }
    ];

    sortFieldOptions: SelectOption<TodoSortField>[] = [
        { label: 'Created date', value: TodoSortField.CreatedAt },
        { label: 'Due date', value: TodoSortField.CompleteUntil },
        { label: 'Title', value: TodoSortField.Title }
    ];

    sortDirectionOptions: SelectOption<SortDirection>[] = [
        { label: 'Newest first', value: SortDirection.Descending },
        { label: 'Oldest first', value: SortDirection.Ascending }
    ];

    onFilterChange() {
        this.filterChanged.emit();
    }

    resetFilters() {
        this.selectedCategory.set(null);
        this.isCompleted.set(null);
        this.completeUntilFrom.set(null);
        this.completeUntilTo.set(null);
        this.sortField.set(TodoSortField.CreatedAt);
        this.sortDirection.set(SortDirection.Descending);
        this.filterChanged.emit();
    }
}
