import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { TodoFilter } from '../interfaces/todo/todo-filter.interface';
import { TodoSorter } from '../interfaces/todo/todo-sorter.interface';
import { PagedResponse } from '../interfaces/common/paged-response.interface';
import { TodoGetResponse } from '../interfaces/todo/todo-get-response.interface';
import { TodoUpdateRequest } from '../interfaces/todo/todo-update-request.interface';
import { TodoCreateRequest } from '../interfaces/todo/todo-create-request.interface';

@Service()
export class TodoService {
    private baseUrl = 'http://localhost:10000/todos'
    private http = inject(HttpClient);

    getAll(
        pageNumber: number = 1, 
        pageSize: number = 10, 
        filter?: TodoFilter | null, 
        sorter?: TodoSorter | null
    ) {
        let params = new HttpParams()
            .set('pageNumber', pageNumber.toString())
            .set('pageSize', pageSize.toString());

        if (filter) {
            if (filter.category) params = params.set('Category', filter.category);
            if (filter.isCompleted !== undefined && filter.isCompleted !== null) {
                params = params.set('IsCompleted', filter.isCompleted.toString());
            }
            if (filter.completeUntilFrom) params = params.set('CompleteUntilFrom', filter.completeUntilFrom);
            if (filter.completeUntilTo) params = params.set('CompleteUntilTo', filter.completeUntilTo);
        }
    
        if (sorter) {
            params = params.set('Field', sorter.field);
            params = params.set('Direction', sorter.direction);
        }
    
        return this.http.get<PagedResponse<TodoGetResponse>>(this.baseUrl, { params });
    }

    getById(id: string) {
        return this.http.get<TodoGetResponse>(`${this.baseUrl}/${id}`);
    }

    create(dto: TodoCreateRequest) {
        return this.http.post<TodoGetResponse>(this.baseUrl, dto);
    }

    update(id: string, dto: TodoUpdateRequest) {
        return this.http.patch<void>(`${`${this.baseUrl}/${id}`}`, dto);
    }

    delete(id: string) {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }

    getCategories() {
        return this.http.get<string[]>(`${this.baseUrl}/categories`);
    }
}
