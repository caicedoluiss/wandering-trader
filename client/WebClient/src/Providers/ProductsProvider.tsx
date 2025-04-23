import { createContext, ReactElement, useContext, useEffect, useMemo, useState } from "react";
import { Product } from "../utils/types";
import { getProducts } from "../api/v1/products";
import { AppLoadingContext } from "./AppLoadingSpinnerProvider";

type ContextType = {
    products: Array<Product>;
};

const defaultValue: ContextType = { products: [] };

export const ProductsContext = createContext<ContextType>(defaultValue);

type Props = {
    children: ReactElement | ReactElement[];
    isAdminUser: boolean;
};

export default function ProductsProvider({ children, isAdminUser }: Props) {
    const { setLoading } = useContext(AppLoadingContext);
    const [products, setProducts] = useState<Array<Product>>([]);
    console.log(isAdminUser);

    const fetchData = async () => {
        setLoading(true);
        try {
            const productsResponse = await getProducts();
            const products = productsResponse.products
                .filter((p) => !p.adminOnlyVisibility || isAdminUser)
                .sort((a, b) => a.name.localeCompare(b.name));
            setProducts(products);
        } catch (error) {
            console.log(error);
            return [];
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        let fetch = true;

        if (fetch) {
            fetch = false;
            fetchData();
        }

        return () => {
            fetch = false;
        };
    }, [isAdminUser]);

    const value = useMemo(() => ({ products }), [products]);

    return <ProductsContext.Provider value={value}>{children}</ProductsContext.Provider>;
}
