import { Product } from "../../utils/types";
import httpClient from "../httpClient";

type GetProductResponse = {
    products: Array<Product>;
};

export async function getProducts(): Promise<GetProductResponse> {
    const url = "/v1/products";
    return httpClient.get(url);
}
