import {
    Box,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography,
} from "@mui/material";
import { OrderItem, Order } from "../../utils/types";
import { CalculationProp, getOrderCalculation, getOrdersCalculation } from "../../utils/ordersCalculations";
import formatMoney from "../../utils/formatMoney";

type Props = {
    orders: Array<Order>;
};

export default function OrdersTable({ orders }: Props) {
    return (
        <Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }}>
                    <TableHead>
                        <TableRow>
                            <TableCell>Hora</TableCell>
                            <TableCell colSpan={2}>Producto</TableCell>
                            <TableCell align="right">Método de pago</TableCell>
                            <TableCell align="right">Subtotal</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {orders.map((o) => (
                            <TableRow key={o.id}>
                                <TableCell>{o.timeMoment.format("HH:mm")}</TableCell>
                                <TableCell colSpan={2} component="th" scope="row">
                                    {o.products
                                        .reduce(
                                            (acc: Array<string>, op: OrderItem) => [
                                                ...acc,
                                                `${op.product.name} x${op.quantity}`,
                                            ],
                                            []
                                        )
                                        .join(" + ")}
                                </TableCell>
                                <TableCell align="right">{o.payment}</TableCell>
                                <TableCell align="right">
                                    {formatMoney(getOrderCalculation(o, CalculationProp.Price))}
                                </TableCell>
                            </TableRow>
                        ))}
                        <TableRow>
                            <TableCell colSpan={3} />
                            <TableCell align="right">
                                <Typography fontStyle="oblique">Total</Typography>
                            </TableCell>
                            <TableCell align="right" colSpan={4}>
                                <Typography fontStyle="oblique">
                                    {formatMoney(getOrdersCalculation(orders, CalculationProp.Price))}
                                </Typography>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
}
