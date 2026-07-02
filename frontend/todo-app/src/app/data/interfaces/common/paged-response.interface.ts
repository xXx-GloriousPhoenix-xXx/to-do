export interface PagedResponse<T> {
    items: T[],
    pageNumber: number,
    pageSize: number,
    pageCount: number,
    totalCount: number,
    hasNext: boolean,
    hasPrevious: boolean
}
