export interface Transaction {
    id: string;
    description: string;
    value: number;
    type: number; // 0 = Despesa, 1 = Receita
    userId: string;
}
