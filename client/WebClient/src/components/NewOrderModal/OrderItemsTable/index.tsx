import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { OrderItem } from "../../../utils/types";
import { Box, IconButton, Typography } from "@mui/material";
import { Delete } from "@mui/icons-material";
import formatMoney from "../../../utils/formatMoney";

type Props = {
    items: Array<OrderItem>;
    onDelete: (index: number) => void;
};

export default function OrderItemsTable({ items, onDelete }: Props) {
    return (
        <Box>
            <Typography variant="h6">Productos</Typography>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Typography>Subtotal:</Typography>
                <Typography>{formatMoney(items.reduce((acc, i) => acc + i.product.price * i.quantity, 0))}</Typography>
            </Box>
            <br />
            <TableContainer component={Paper} sx={{ width: 0.5, minWidth: "300px" }}>
                <Table aria-label="simple table" size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell>Producto</TableCell>
                            <TableCell align="right">Cantidad</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {items?.map((row, index) => (
                            <TableRow key={row.id}>
                                <TableCell>{row.product.name}</TableCell>
                                <TableCell rowSpan={1} align="right">
                                    {row.quantity}
                                </TableCell>
                                <TableCell align="right">
                                    <IconButton color="error" onClick={(_) => onDelete(index)}>
                                        <Delete />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
}
