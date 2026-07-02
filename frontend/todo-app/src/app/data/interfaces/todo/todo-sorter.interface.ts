import { SortDirection } from "../common/sort-direction";
import { TodoSortField } from "./todo-sort-field";

export interface TodoSorter {
    field: TodoSortField;
    direction: SortDirection;
}
