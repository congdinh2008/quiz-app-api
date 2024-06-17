export class PaginatedResult<T> {
    items: T[] = [];
    pageNumber: number = 1;
    pageSize: number = 10;
    totalCount: number = 0;
    totalPages: number = 0;
}