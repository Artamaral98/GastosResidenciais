import { useState, useEffect } from "react";
import api from "../../api/api";
import { toast } from "react-hot-toast";
import { Transaction } from "../Models/Types";


const useTransactions = () => {
    const [transactions, setTransactions] = useState<Transaction[]>([]);
    const [newTransaction, setNewTransaction] = useState({ type: "", description: "", valor: "" });
    const [selectedUser, setSelectedUser] = useState("");
    const [description, setDescription] = useState("");
    const [transactionType, setTransactionType] = useState("");
    const [value, setValue] = useState("");
    const [transactionsModalOpen, setTransactionsModalOpen] = useState<boolean>(false);
    const [userTransactions, setUserTransactions] = useState<Transaction[]>([]);
    const [selectedTransaction, setSelectedTransaction] = useState<number | null>(null);
    const [deleteTransactionModalOpen, setDeleteTransactionModalOpen] = useState<boolean>(false)
    const [isEditModalOpen, setIsEditModalOpen] = useState<boolean>(false)

    const [editTransaction, setEditTransaction] = useState<{
        id: number | null;
        type: number | null;
        description: string;
        valor: number | null;
    }>({
        id: null,
        type: null,
        description: "",
        valor: null,
    });

    


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
            //insere a nova transaçãona lista
            setTransactions([...transactions, response.data]);
            //limpar os campos após o envio
            setSelectedUser("");
            setDescription("");
            setTransactionType("");
            setValue("");
            toast.success("Transação criada com sucesso!");

            //casos para cada erro de validação
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

    //Enviar o formulário para criação de usuarios
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

    //método para deletar transãções
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

    //método que identificar a transação a ser modificada, e insere seus dados em TransactionToEdit
    const transactionToBeEdit = async (id: number) => {
        const TransactionToEdit = transactions.find(transaction => transaction.id === id)

        if (TransactionToEdit) {
            setEditTransaction({
                id: TransactionToEdit.id,
                description: TransactionToEdit.description,
                type: TransactionToEdit.types,
                valor: TransactionToEdit.valor

            })
            setIsEditModalOpen(true)
        }
    }

    //função que realiza a alteração dos dados da transação.
    const updateTransaction = async (id: number): Promise<boolean> => { //o boolean vai ser util para fazer o modal fechar caso as validações tenham sido confirmadas
        try {
            const response = await api.put(`/transaction/`, {
                description: String(editTransaction.description),
                valor: Number(editTransaction.valor),
                types: Number(editTransaction.type),
                id: Number(editTransaction.id),
            });

            //alterando o estado da lista de transações, adicionando a nova transação
            const updatedTransactions = transactions.map((transaction) =>
                transaction.id === id ? { ...transaction, ...response.data } : transaction
            );

            setTransactions(updatedTransactions);

            toast.success("Transação atualizada com sucesso!");
            return true;

        } catch (error: any) {
            console.log(error);
            toast.error(error.response?.data?.errors?.[0] || "Erro ao atualizar transação.");
            return false;
        }
    }; 

    //Abrir o modal de transações do usuário
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

    //fechar o modal de transações do usuário
    const closeTransactionsModal = () => {
        setTransactionsModalOpen(false);
        setUserTransactions([]);
    }

    //Abrir o modal de deletar transações
    const openDeleteTransactionModal = (id: number) => {
        setSelectedTransaction(id)
        setDeleteTransactionModalOpen(true)
    };

    //Fechar o modal de deletar transações
    const closeTransactionDeleteModal = () => {
        setSelectedTransaction(null);
        setDeleteTransactionModalOpen(false)
    };


    //Função contida no botão de confirmar deleção.
    const confirmDeleteTransaction = async () => {
        if (selectedTransaction !== null) {
            await deleteTransaction(selectedTransaction);
            closeTransactionDeleteModal();
        }
    };

    //fechar modal de atualizar transação
    //const closeTransactionUpdateModal = async () => {
    //    if (isEditModalOpen) {
    //        setIsEditModalOpen(false)
    //    }
    //}

    const openTransactionUpdateModal = (id: number) => {
        const transactionToEdit = transactions.find(transaction => transaction.id === id);

        if (transactionToEdit) {
            setEditTransaction({
                id: transactionToEdit.id,
                description: transactionToEdit.description,
                type: transactionToEdit.types,
                valor: transactionToEdit.valor
            });
            setIsEditModalOpen(true);
        }
    };

    // Método para fechar o modal de atualização
    const closeTransactionUpdateModal = () => {
        setIsEditModalOpen(false);
        setEditTransaction({
            id: null,
            type: null,
            description: "",
            valor: null,
        });
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
        selectedTransaction,
        updateTransaction,
        transactionToBeEdit,
        isEditModalOpen,
        editTransaction,
        closeTransactionUpdateModal,
        setEditTransaction,
        fetchTransactions,
        openTransactionUpdateModal,
        closeTransactionUpdateModal
    };
};

export default useTransactions;