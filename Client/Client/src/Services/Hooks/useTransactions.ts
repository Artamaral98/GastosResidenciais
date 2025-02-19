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
        id: number | null
        type: number | null
        description: string
        valor: number | null
        userId: number | null
    }>({
        id: null,
        type: null,
        description: "",
        valor: null,
        userId: null
    })

    


    //buscar transações da API
    const fetchTransactions = async () => {
        try {
            const response = await api.get("/transaction/all");
            setTransactions(response.data);

        } catch (error) {
            toast.error("Erro ao buscar Transações");
        }
    };

    //carrega as transações após montar o componente
    useEffect(() => {
        fetchTransactions();
    }, [])



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
            console.log(error)
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
        )

    }

    //método para deletar transãções
    const deleteTransaction = async (id: number) => {
        try {
            const response = await api.delete(`/transaction/${id}`)
            setTransactions(transactions.filter(transaction => transaction.id !== id))
            toast.success(response.data.message);

        } catch (error) {
            toast.error(error.response.data.message)
        }
    }

    //método que identifica a transação a ser modificada
    const transactionToBeEdit = async (id: number) => {
        // Encontrar a transação que está sendo editada
        const transactionToEdit = transactions.find(transaction => transaction.id === id);
        if (!transactionToEdit) return;

        // Verificar se o usuário pode editar a transação com base na idade
        const isAgeValid = await verifyAgeToEdit(transactionToEdit);
        if (!isAgeValid) return;

        // Se a verificação da idade passar, preencher os dados para edição
        setEditTransaction({
            id: transactionToEdit.id,
            description: transactionToEdit.description,
            type: transactionToEdit.types,
            valor: transactionToEdit.valor,
            userId: transactionToEdit.userId
        });

        setIsEditModalOpen(true);
    }


    //função que realiza a alteração dos dados da transação.
    const updateTransaction = async (id: number): Promise<boolean> => { //o boolean vai ser util para fazer o modal fechar caso as validações tenham sido confirmadas
        try {
            const response = await api.put(`/transaction/`, {
                description: String(editTransaction.description),
                valor: Number(editTransaction.valor),
                types: Number(editTransaction.type),
                id: Number(editTransaction.id),
                userId: Number(editTransaction.userId),
                updatedAt: new Date().toLocaleString() //insere a hora atual como string no UpdatedAt
            });


            //alterando o estado da lista de transações, adicionando a nova transação
            const updatedTransactions = transactions.map((transaction) =>
                transaction.id === id ? { ...transaction, ...response.data } : transaction
            );

            setTransactions(updatedTransactions);

            toast.success("Transação atualizada com sucesso!");
            return true;

        } catch (error: any) {
            toast.error(error.response?.data?.errors?.[0] || "Erro ao atualizar transação.");
            return false;
        }
    };

    //Abrir o modal de transações do usuário
    const openTransactionsModal = async (userId: number) => {
        try {
            const response = await api.get<Transaction[]>(`/transaction/${userId}`);
            setUserTransactions(response.data);
            setTransactionsModalOpen(true);

        } catch (error) {
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
    }


    const openTransactionUpdateModal = (id: number) => {
        const transactionToEdit = transactions.find(transaction => transaction.id === id);

        if (transactionToEdit) {
            setEditTransaction({
                id: transactionToEdit.id,
                description: transactionToEdit.description,
                type: transactionToEdit.types,
                valor: transactionToEdit.valor,
                userId: transactionToEdit.userId
            });
            setIsEditModalOpen(true);
        }
    }

    // Método para fechar o modal de atualização
    const closeTransactionUpdateModal = () => {
        setIsEditModalOpen(false);
        setEditTransaction({
            id: null,
            type: null,
            description: "",
            valor: null,
            userId: null
        });
    }

    //Método que impede a inserção de caracteres especiais e modifica o estado do input
    const handleChangeTransaction = (e: React.ChangeEvent<HTMLInputElement>) => {
        const filteredValue = e.target.value.replace(/[^a-zA-ZÀ-ÿ0-9\s]/g, ""); 
        setEditTransaction((prev) => ({ ...prev, description: filteredValue }));
    };


    //método para verificar a idade da transação do usuário
    const verifyAgeToEdit = async (transactionToEdit: Transaction) => {
        try {
            //obter os dados do usuario baseado no userid da transação
            const userResponse = await api.get(`user/${transactionToEdit.userId}`);
            const age = userResponse.data.age;

            //verificar se a modificação é de 'expense' para 'revenue'
            if (transactionToEdit.types === "expense" && editTransaction.type === "revenue") {
                if (age < 18) {
                    toast.error("Usuários com menos de 18 anos não podem alterar uma transação de despesa para receita.");
                    return false;
                }
            }

            return true; // Se tudo estiver ok, pode continuar a edição
        } catch (error) {
            return false;
        }
    }



    return {
        transactions,
        createTransaction,
        selectedUser,
        setSelectedUser,
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
        setEditTransaction,
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
        openTransactionUpdateModal,
        handleChangeTransaction,
    };
};

export default useTransactions;