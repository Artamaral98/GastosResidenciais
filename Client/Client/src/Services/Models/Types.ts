export type GlobalTotal = {
    totalRevenue: number;
    totalExpenses: number
    valor: number;
};

export type UsersResponse = {
    users: User[];
    globalTotal: GlobalTotal
};

export interface User {
    id: number
    name: string
    age: number
}

export interface Transaction {
    id: number
    description: string
    valor: number
    types: number;
    userId: number
    date: string
    updatedAt: string | null;
}

export interface TransactionsModalProps {
    isOpen: boolean
    onClose: () => void
    transactions: Transaction[]
}

export interface DeleteModalProps {
    isOpen: boolean;
    onClose: () => void //Vai receber o m�todo closeDeleteModal
    onConfirm: () => void//Vai receber o m�todo confirmDelete
}


export interface UpdateTransactionModalProps {
    isOpen: boolean
    transaction: { id: number | null; description: string; type: number | null; valor: number | null; userId: number | null };
    closeModal: () => void
}