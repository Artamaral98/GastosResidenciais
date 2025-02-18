import { useState, useEffect } from "react";
import api from "../../api/api";
import toast from "react-hot-toast";
import { GlobalTotal, UsersResponse, User } from "../Types/Types"


const useUsers = () => {
    const [users, setUsers] = useState<User[]>([]); // Estado para armazenar a lista de usuários
    const [newUser, setNewUser] = useState({ name: "", age: "" });
    const [loading, setLoading] = useState<boolean>(true); // Estado para controle de carregamento
    const [selectedUser, setSelectedUser] = useState<number | null>(null);
    const [globalTotal, setGlobalTotal] = useState<GlobalTotal>({
        totalRevenue: 0,
        totalExpenses: 0,
        valor: 0,
    });

    //buscar todos os usuários
    const fetchUsers = async () => {
        setLoading(true) 
        try {
            const response = await api.get<UsersResponse>("/user/all");
            setUsers(response.data.users); //Insere os usuários na lista de usuários
            setLoading(false) 
            setGlobalTotal(response.data.globalTotal); //Insere as informações de calculos dentro da lista

        } catch (error) {
            toast.error("Erro ao carregar usuários")
            setLoading(false);
        }
    };

    //criar um novo usuário
    const createUser = async (name: string, age: number) => {
        try {
            const response = await api.post("/user", { name, age });
            setUsers([...users, response.data]);
            setNewUser({ name: "", age: "" });
            toast.success("Usuário criado com sucesso!");
        } catch (error: any) {
            toast.error(error.response.data.errors[0] || error.response.data.errors[1]);
        }
    };

    //deletar um usuário
    const deleteUser = async (userId: number) => {
        try {
            await api.delete(`/user/${userId}`);
            setUsers(users.filter(user => user.id !== userId));
            toast.success("Usuário deletado com sucesso!");
        } catch (error) {
            toast.error("Erro ao deletar usuário.");
        }
    };

    //abrir o modal de confirmação de deleção
    const openDeleteModal = (userId: number) => {
        setSelectedUser(userId);
    };

    //confirmar a deleção
    const confirmDelete = async () => {
        if (selectedUser !== null) {
            await deleteUser(selectedUser);
            closeDeleteModal();
        }
    };

    //fechar o modal de confirmação
    const closeDeleteModal = () => {
        setSelectedUser(null);
    };

    //Função que lida com as mudanças nos campos do formulário
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        setNewUser(prevState => ({
            ...prevState,
            [id]: value,
        }));
    };

    //função que lida com o envio do formulário
    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await createUser((String(newUser.name)), Number(newUser.age));
    };

    //carrega os usuários ao montar o componente
    useEffect(() => {
        fetchUsers();
    }, []);


    return {
        users,
        setUsers,
        loading,
        fetchUsers,
        newUser,
        handleChange,
        handleSubmit,
        openDeleteModal,
        confirmDelete,
        closeDeleteModal,
        selectedUser,
        globalTotal,
    };
};

export default useUsers;
