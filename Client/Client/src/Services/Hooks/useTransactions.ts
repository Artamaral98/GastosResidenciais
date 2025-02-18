import { useState, useEffect } from "react";
import api from "../../api/api";
import { toast } from "react-hot-toast";
import { Transaction } from "../Types/Types";


const useTransactions = () => {
    const [transactions, setTransactions] = useState<Transaction[]>([]);
    const [newTransaction, setNewTransaction] = useState({ type: "", description: "", valor: "" });
    const [selectedUser, setSelectedUser] = useState("");
    const [description, setDescription] = useState("");
    const [transactionType, setTransactionType] = useState("");
    const [value, setValue] = useState("");
    const [transactionsModalOpen, setTransactionsModalOpen] = useState<boolean>(false);
    const [userTransactions, setUserTransactions] = useState<Transaction[]>([]);
    const [selectedTransaction, setSelectedTransaction] = useState < number | null>(null);
    const [deleteTransactionModalOpen, setDeleteTransactionModalOpen] = useState<boolean>(false)

    //buscar transações da API
    const fetchTransactions = async () => {
        try {
            const response = await api.get("/transaction/all");
            setTransactions(response.data);

        } catch (error) {
            toast.error("Erro ao buscar Transações");
        }
    };


    //criar uma nova transação
    const createTransaction = async (
        userId: number,
        description: string,
        valor: number,
        types: number
    ) => {
        try {
            const response = await api.post("/transaction", {
                description,
                valor,
                types,
                userId,
   
            });
            setTransactions([...transactions, response.data]);
            //limpar os campos após o envio
            setSelectedUser("");
            setDescription("");
            setTransactionType("");
            setValue("");
            toast.success("Transação criada com sucesso!");

        } catch (error: any) {
            if (error.response.data.errors[0]) {
                toast.error(error.response.data.errors[0])
            }

            if (error.response.data.errors[1]) {
                toast.error(error.response.data.errors[1])
            }

            if (error.response.data.errors[2]) {
                toast.error(error.response.data.errors[2])
            }

            if (error.response.data.errors[3]) {
                toast.error(error.response.data.errors[3])
            }

            if (error.response.data.errors[4]) {
                toast.error(error.response.data.errors[4])
            }

        }

    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        //criar a transação com os dados preenchidos
        await createTransaction(
            Number(selectedUser),
            String(description),
            Number(value),
            Number(transactionType)
        );

    };

    const deleteTransaction = async (id: number) => {
        try {
            const response = await api.delete(`/transaction/${id}`)
            setTransactions(transactions.filter(transaction => transaction.id !== id))
            console.log()
            toast.success(response.data.message);

        } catch (error) {
            console.log(error.response)
            toast.error(error.response.data.message)
        }
    }

    const openTransactionsModal = async (userId: number) => {
        try {
            const response = await api.get<Transaction[]>(`/transaction/${userId}`);
            setUserTransactions(response.data);
            console.log(response)
            setTransactionsModalOpen(true);
        } catch (error) {
            console.log(error)
            toast.error("Erro ao carregar transações.");
        }
    }

    const closeTransactionsModal = () => {
        setTransactionsModalOpen(false);
        setUserTransactions([]);
    }

    const openDeleteTransactionModal = (id: number) => {
        setSelectedTransaction(id)
        setDeleteTransactionModalOpen(true)
    };

    const closeTransactionDeleteModal = () => {
        setSelectedTransaction(null);
        setDeleteTransactionModalOpen(false)
    };

    const confirmDeleteTransaction = async () => {
        if (selectedTransaction !== null) {
            await deleteTransaction(selectedTransaction);
            closeTransactionDeleteModal();
        }
    };

    //carrega as transações após montar o componente
    useEffect(() => {
        fetchTransactions();
    }, []);


    return {
        transactions,
        createTransaction,
        selectedUser,
        setSelectedUser,
        newTransaction,
        setNewTransaction,
        description,
        setDescription,
        transactionType,
        setTransactionType,
        value,
        setValue,
        handleSubmit,
        openTransactionsModal,
        closeTransactionsModal,
        transactionsModalOpen,
        userTransactions,
        deleteTransaction,
        openDeleteTransactionModal,
        confirmDeleteTransaction,
        closeTransactionDeleteModal,
        selectedTransaction
    };
};

export default useTransactions;