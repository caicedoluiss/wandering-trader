import Box from "@mui/material/Box";
import Fab from "@mui/material/Fab";
import AddIcon from "@mui/icons-material/Add";

type Props = {
    onClick: () => void;
};

export default function FloatingAddButton({ onClick }: Props) {
    return (
        <Box
            sx={{
                "& > :not(style)": { m: 1 },
                bottom: 0,
                right: 0,
                position: "fixed",
                mb: 2,
                opacity: 0.75,
            }}
        >
            <Fab color="primary" aria-label="add" sx={{ width: 70, height: 70 }} onClick={onClick}>
                <AddIcon />
            </Fab>
        </Box>
    );
}
