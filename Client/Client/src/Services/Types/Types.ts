export type GlobalTotal = {
    totalRevenue: number;
    totalExpenses: number;
    valor: number;
};

export type UsersResponse = {
    users: User[];
    globalTotal: GlobalTotal;
};

export interface User {
    id: number;
    name: string;
    age: number;
}

export interface Transaction {
    id: number;
    description: string;
    value: number;
    type: number; 
    userId: number;
    date: string
}

export interface TransactionsModalProps {
    isOpen: boolean;
    onClose: () => void;
    transactions: Transaction[];
}

export interface DeleteModalProps {
    isOpen: boolean;
    onClose: () => void; //Vai receber o método closeDeleteModal
    onConfirm: () => void;//Vai receber o método confirmDelete
}