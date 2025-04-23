import { useContext, useEffect, useState } from "react";
import {
    Box,
    Button,
    Checkbox,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Divider,
    FormControl,
    FormControlLabel,
    InputLabel,
    MenuItem,
    Select,
    Typography,
} from "@mui/material";
import QuantityModal from "./QuantityDialog";
import { OrderItem, Product, Order, PaymentMethod } from "../../utils/types";
import moment from "moment";
import OrderItemsTable from "./OrderItemsTable";
import newUuid from "../../utils/newUuid";
import { ProductsContext } from "../../Providers/ProductsProvider";
import AppDatePicker from "../AppDatePicker";

type Props = {
    open: boolean;
    disablePastDates: boolean;
    onAdd: (saleOrder: Order) => void;
    onClose: () => void;
};

export default function NewOrderModal({ open, disablePastDates, onAdd, onClose }: Props) {
    const { products } = useContext(ProductsContext);
    const [selectedDate, setSelectedDate] = useState<Date | null>(new Date());
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
    const [paymentMethod, setPaymentMethod] = useState<PaymentMethod>(PaymentMethod.Efectivo);
    const [orderItems, setProductOrders] = useState<Array<OrderItem>>([]);
    const [noCharge, setNoCharge] = useState<boolean>(false);

    useEffect(() => {
        setSelectedDate(new Date());
        setPaymentMethod(PaymentMethod.Efectivo);
        setProductOrders([]);
        setSelectedProduct(null);
    }, [open]);

    const handleSetQuantity = (quantity: number) => {
        if (!Boolean(selectedProduct)) return;
        setProductOrders([...orderItems, { id: newUuid(), product: selectedProduct as Product, quantity }]);
    };

    const handleSelectProduct = (productId: string) => {
        if (Boolean(productId) && productId !== "-1") {
            const product = products.find((p) => p.id === productId) || null;
            setSelectedProduct(product);
        } else {
            setSelectedProduct(null);
        }
    };

    const handleDeleteProduct = (index: number) => {
        if (index >= 0) {
            const arr = [...orderItems.slice(0, index), ...orderItems.slice(index + 1, orderItems.length)];
            setProductOrders(arr);
        }
    };

    const handleClose = () => {
        onClose();
    };

    const handleAdd = () => {
        if (orderItems.length === 0) {
            alert("Debe agregar al menos un producto");
            return;
        }

        if (!selectedDate || (!(selectedDate instanceof Date) && !isNaN(selectedDate))) {
            alert("Fecha no valida");
            return;
        }

        const dateMoment = moment(selectedDate);
        const order: Order = {
            id: newUuid(),
            timeMoment:
                dateMoment.diff(moment(), "days") < 0
                    ? dateMoment.set({
                          hour: 0,
                          minute: 0,
                          second: 0,
                          millisecond: 0,
                      })
                    : dateMoment,
            noCharge,
            products: orderItems,
            payment: !noCharge ? paymentMethod : null,
        };
        onAdd(order);
        handleClose();
    };

    return (
        <>
            <QuantityModal
                open={selectedProduct != null}
                product={selectedProduct}
                onSet={handleSetQuantity}
                onClose={() => setSelectedProduct(null)}
            />
            <Dialog open={open} onClose={onClose} fullScreen>
                <DialogTitle>Registrar Orden de venta</DialogTitle>
                <DialogContent>
                    <FormControl size="small" sx={{ py: 2 }}>
                        <AppDatePicker
                            value={selectedDate}
                            disablePast={disablePastDates}
                            onChange={(date) => setSelectedDate(date)}
                        />
                    </FormControl>
                    <br />
                    {!noCharge && (
                        <>
                            <FormControl size="small" sx={{ width: 0.4 }}>
                                <InputLabel>Método de pago</InputLabel>
                                <Select
                                    label="Método de pago"
                                    value={paymentMethod}
                                    onChange={(e) =>
                                        setPaymentMethod(PaymentMethod[e.target.value as keyof typeof PaymentMethod])
                                    }
                                >
                                    <MenuItem value={PaymentMethod.Efectivo}>Efectivo</MenuItem>
                                    <MenuItem value={PaymentMethod.Nequi}>Nequi</MenuItem>
                                </Select>
                            </FormControl>
                            <br />
                        </>
                    )}
                    <FormControlLabel
                        control={<Checkbox checked={noCharge} onChange={(e) => setNoCharge(e.target.checked)} />}
                        label="Sin Cargo"
                    />
                    <Divider />
                    <br />
                    <FormControl size="small" fullWidth>
                        <Select
                            value={selectedProduct?.id || "-1"}
                            onChange={(e) => handleSelectProduct(e.target.value)}
                        >
                            <MenuItem value={"-1"}>
                                <Typography component="i">&lt; Seleccione el producto a agregar &gt;</Typography>
                            </MenuItem>
                            {products.map((p) => (
                                <MenuItem key={p.id} value={p.id}>
                                    {p.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Box>
                        <br />
                        <OrderItemsTable items={orderItems} onDelete={handleDeleteProduct} />
                    </Box>
                </DialogContent>
                <DialogActions>
                    <Button onClick={onClose}>Cancelar</Button>
                    <Button variant="contained" color="primary" onClick={handleAdd}>
                        Agregar
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
