import { Order, PaymentMethod } from "./types";

export enum CalculationProp {
    Price = "price",
    Cost = "cost",
}

export function getOrdersCalculation(
    orders: Array<Order>,
    property: CalculationProp,
    paymentMethod: PaymentMethod | null = null
): number {
    return orders
        .filter((o) => (!paymentMethod ? true : o.payment === paymentMethod))
        .reduce((acc, o) => acc + getOrderCalculation(o, property, paymentMethod), 0);
}

export function getOrderCalculation(
    order: Order,
    property: CalculationProp,
    paymentMethod: PaymentMethod | null = null
): number {
    return !paymentMethod || paymentMethod === order.payment
        ? order.products.reduce(
              (acc, p) =>
                  acc +
                  p.product[property] * p.quantity * (order.noCharge && property === CalculationProp.Price ? 0 : 1),
              0
          )
        : 0;
}
