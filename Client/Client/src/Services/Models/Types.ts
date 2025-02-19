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
    valor: number;
    types: number; 
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


export interface UpdateTransactionModalProps {
    isOpen: boolean;
    transaction: { id: number | null; description: string; type: number | null; valor: number | null };
    closeModal: () => void;
    updateTransaction: () => void;
    setEditTransaction: React.Dispatch<React.SetStateAction<{
        id: number | null;
        type: number | null;
        description: string;
        valor: number | null;
    }>>;
}