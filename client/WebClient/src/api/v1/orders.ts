import moment, { Moment } from "moment";
import { Order } from "../../utils/types";
import httpClient from "../httpClient";

type ListOrdersResponse = { orders: Array<Order> };
type PostOrderResponse = { id: string };

export async function listOrders(date: Moment): Promise<ListOrdersResponse> {
    const url = `/v1/orders/${date.format()}`;
    const res: {
        orders: Array<{
            id: string;
            date: Date;
            noCharge: boolean;
            paymentMethod: string | null;
            items: Array<{ id: string; name: string; cost: number; price: number; quantity: number }>;
        }>;
    } = await httpClient.get(url);

    return {
        orders: res.orders.map((o) => ({
            id: o.id,
            timeMoment: moment(o.date),
            noCharge: o.noCharge,
            payment: o.paymentMethod,
            products: o.items.map((i) => ({
                id: i.id,
                quantity: i.quantity,
                product: { id: i.id, name: i.name, cost: i.cost, price: i.price },
            })),
        })),
    };
}

export function createOrder(order: Order): Promise<PostOrderResponse> {
    const url = "/v1/orders";
    const body = {
        clientDate: order.timeMoment.format(),
        noCharge: order.noCharge,
        paymentMethod: order.payment,
        items: order.products.map((i) => ({
            id: i.id,
            name: i.product.name,
            cost: i.product.cost,
            price: i.product.price,
            quantity: i.quantity,
        })),
    };

    return httpClient.post(url, body);
}
