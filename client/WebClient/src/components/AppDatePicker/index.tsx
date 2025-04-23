import { es } from "date-fns/locale/es";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFnsV3";

type Props = {
    value: Date | null;
    onChange: (date: Date | null) => void;
    disablePast: boolean;
};

export default function AppDatePicker({ value, onChange, disablePast }: Props) {
    return (
        <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={es}>
            <DatePicker
                label="Fecha"
                slotProps={{ textField: { size: "small" } }}
                value={value}
                onChange={(date) => onChange(date)}
                disablePast={disablePast}
                disableFuture
            />
        </LocalizationProvider>
    );
}
