import { Moment } from "moment";

export type Product = {
    id: string;
    name: string;
    cost: number;
    price: number;
    adminOnlyVisibility: boolean;
};

export type OrderItem = {
    id: string;
    product: Product;
    quantity: number;
};

export type Order = {
    id: string;
    timeMoment: Moment;
    noCharge: boolean;
    products: Array<OrderItem>;
    payment: PaymentMethod | null;
};

export enum PaymentMethod {
    Efectivo = "Efectivo",
    Nequi = "Nequi",
}
