import { Backdrop, CircularProgress } from "@mui/material";
import { createContext, ReactElement, useState } from "react";

type ContextType = {
    loading: boolean;
    setLoading: (value: boolean) => void;
};

const defaultValue: ContextType = { loading: false, setLoading: (_) => {} };

export const AppLoadingContext = createContext<ContextType>(defaultValue);

type Props = {
    children: ReactElement | ReactElement[];
};

export default function AppLoadingSpinnerProvider({ children }: Props) {
    const [loading, setLoading] = useState<boolean>(false);
    return (
        <AppLoadingContext.Provider value={{ loading, setLoading }}>
            <Backdrop open={loading} sx={(theme) => ({ color: "#fff", zIndex: theme.zIndex.drawer + 1 })}>
                <CircularProgress color="inherit" />
            </Backdrop>
            {children}
        </AppLoadingContext.Provider>
    );
}
