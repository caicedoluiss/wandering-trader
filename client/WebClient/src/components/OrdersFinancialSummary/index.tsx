import { Box, Paper, Table, TableBody, TableCell, TableContainer, TableRow } from "@mui/material";
import { Order, PaymentMethod } from "../../utils/types";
import { useMemo } from "react";
import formatMoney from "../../utils/formatMoney";
import { CalculationProp, getOrdersCalculation } from "../../utils/ordersCalculations";

type Props = {
    orders: Array<Order>;
};
export default function OrdersFinancialSummary({ orders }: Props) {
    const totalEfectivo = useMemo(
        () => getOrdersCalculation(orders, CalculationProp.Price, PaymentMethod.Efectivo),
        [orders]
    );
    const totalNequi = useMemo(
        () => getOrdersCalculation(orders, CalculationProp.Price, PaymentMethod.Nequi),
        [orders]
    );
    const totalSales = useMemo(() => getOrdersCalculation(orders, CalculationProp.Price), [orders]);
    const totalCosts = useMemo(() => getOrdersCalculation(orders, CalculationProp.Cost), [orders]);
    const balance = useMemo(() => totalSales - totalCosts, [orders]);
    const margin = useMemo(() => (balance / totalSales) * 100 || 0, [balance, totalSales]);
    const roi = useMemo(() => (balance / totalCosts) * 100 || 0, [balance, totalCosts]);

    return (
        <Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableBody>
                        <TableRow>
                            <TableCell>Total Efectivo</TableCell>
                            <TableCell>{formatMoney(totalEfectivo)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Total Nequi</TableCell>
                            <TableCell>{formatMoney(totalNequi)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Total Ventas</TableCell>
                            <TableCell>{formatMoney(totalSales)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Total Costos (+25%)</TableCell>
                            <TableCell>{formatMoney(totalCosts)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Balance</TableCell>
                            <TableCell>{formatMoney(balance)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Margen</TableCell>
                            <TableCell>{`${margin.toFixed(2)} %`}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>ROI</TableCell>
                            <TableCell>{`${roi.toFixed(2)} %`}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
}
