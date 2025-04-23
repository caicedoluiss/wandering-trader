const formatter = new Intl.NumberFormat("es-CO", {
    style: "currency",
    currency: "COP",
    minimumFractionDigits: 0,
});

export default function formatMoney(amount: number): string {
    return formatter.format(amount);
}
