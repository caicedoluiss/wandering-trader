import { Box, FormControl, Typography } from "@mui/material";
import OrdersTable from "./components/OrdersTable";
import FloatingAddButton from "./components/FloatingAddButton";
import NewOrderModal from "./components/NewOrderModal";
import { Order } from "./utils/types";
import { useContext, useEffect, useState } from "react";
import { createOrder, listOrders } from "./api/v1/orders";
import AppDatePicker from "./components/AppDatePicker";
import moment from "moment";
import AppLoadingSpinnerProvider, { AppLoadingContext } from "./Providers/AppLoadingSpinnerProvider";
import ProductsProvider from "./Providers/ProductsProvider";
import OrdersFinancialSummary from "./components/OrdersFinancialSummary";

function App() {
    const [isAdmin, setIsAdmin] = useState<boolean>(false);
    const { setLoading } = useContext(AppLoadingContext);
    const [showAddModal, setShowAddModal] = useState<boolean>(false);
    const [orders, setOrders] = useState<Array<Order>>([]);
    const [date, setDate] = useState<Date>(new Date());

    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        setIsAdmin(params.get("admin") === "true");
    }, []);

    const fetchOrders = async (date: Date) => {
        setLoading(true);
        try {
            const r = await listOrders(moment(date));
            setOrders(r.orders);
        } catch (error) {
            console.error(error);
            return [];
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        let fetch = true;
        if (fetch) {
            fetch = false;
            fetchOrders(date);
        }

        return () => {
            fetch = false;
        };
    }, []);

    const handleDateChange = (date: Date) => {
        setLoading(true);
        setDate(date);
        fetchOrders(date);
    };

    const handleAddOrder = async (order: Order) => {
        setLoading(true);
        try {
            await createOrder(order);
            if (moment(date).diff(order.timeMoment, "days") == 0) setOrders([...orders, order]);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box id="app" p={1}>
            <AppLoadingSpinnerProvider>
                <ProductsProvider isAdminUser={isAdmin}>
                    <>
                        {showAddModal && (
                            <NewOrderModal
                                open={showAddModal}
                                disablePastDates={!isAdmin}
                                onAdd={handleAddOrder}
                                onClose={() => setShowAddModal(false)}
                            />
                        )}
                    </>
                    <Box>
                        <FormControl size="small" sx={{ py: 2 }}>
                            <AppDatePicker
                                value={date}
                                onChange={(d) => handleDateChange(d ?? new Date())}
                                disablePast={!isAdmin}
                            />
                        </FormControl>
                    </Box>
                    <>
                        {isAdmin && (
                            <>
                                <Typography variant="h5">Resumen</Typography>
                                <OrdersFinancialSummary orders={orders} />
                                <br />
                            </>
                        )}
                    </>
                    <Typography variant="h5">Productos</Typography>
                    <OrdersTable orders={orders} />
                    <FloatingAddButton onClick={() => setShowAddModal(true)} />
                </ProductsProvider>
            </AppLoadingSpinnerProvider>
        </Box>
    );
}

export default App;
