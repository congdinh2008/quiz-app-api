import { OrderDirection } from "../enums/order-direction.enum";

export class FilterViewModel{
    keyword: string = '';
    pageNumber: number = 1;
    pageSize: number = 10;
    orderBy: string = 'name';
    orderDiection: OrderDirection = OrderDirection.Asc;
}
