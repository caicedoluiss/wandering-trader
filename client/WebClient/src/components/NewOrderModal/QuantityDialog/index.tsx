import {
    Box,
    Typography,
    DialogTitle,
    Dialog,
    DialogContent,
    Button,
    DialogActions,
    Divider,
} from "@mui/material";
import { Add, Cancel, CheckCircle, Remove } from "@mui/icons-material";
import { useState } from "react";
import { Product } from "./../../../utils/types";

type Props = {
    open: boolean;
    product: Product | null;
    onSet: (quantity: number) => void;
    onClose: () => void;
};

export default function QuantityModal({
    open,
    product,
    onSet,
    onClose,
}: Props) {
    const [quantity, setQuantity] = useState<number>(1);

    const handleQuantityChange = (delta: 1 | -1): void => {
        const value = quantity + delta;
        setQuantity(value < 1 ? 1 : value);
    };

    const handleClose = () => {
        setQuantity(1);
        onClose();
    };

    const handleAdd = () => {
        onSet(quantity);
        handleClose();
    };

    if (!product) return null;

    return (
        <Dialog open={open} onClose={handleClose} fullWidth>
            <DialogTitle>Cantidad</DialogTitle>
            <Divider />
            <DialogContent sx={{ textAlign: "center", my: 1 }}>
                <Typography variant="h6">Producto</Typography>
                <br />
                <Typography variant="body1" fontStyle="italic">
                    {product.name}
                </Typography>
                <br />
                <br />
                <Box
                    sx={{
                        display: "flex",
                        width: 1,
                        justifyContent: "space-evenly",
                    }}
                >
                    <Button
                        variant="contained"
                        color="error"
                        onClick={(_) => handleQuantityChange(-1)}
                    >
                        <Remove />
                    </Button>
                    <Typography sx={{ alignSelf: "center" }} variant="h4">
                        x{quantity}
                    </Typography>
                    <Button
                        variant="contained"
                        onClick={(_) => handleQuantityChange(1)}
                    >
                        <Add />
                    </Button>
                </Box>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button variant="outlined" color="error" onClick={handleClose}>
                    <Cancel />
                </Button>
                <Button variant="contained" color="primary" onClick={handleAdd}>
                    <CheckCircle />
                </Button>
            </DialogActions>
        </Dialog>
    );
}
