import React from "react";
import Sidebar from "../Components/Sidebar";
import TransactionsModal from "../Components/Modals/TransactionsModal";
import useUsers from "../Services/Hooks/useUsers";
import { FaTrash, FaEye } from "react-icons/fa";
import DeleteModal from "../Components/Modals/DeleteModal";
import Footer from "../Components/Footer";
import useTransactions from "../Services/Hooks/useTransactions";

const Users: React.FC = () => {
    const {
        users,
        openDeleteModal,
        confirmDelete,
        closeDeleteModal,
        selectedUser
    } = useUsers();

    const { openTransactionsModal,
        closeTransactionsModal,
        userTransactions,
        transactionsModalOpen
    } = useTransactions();

    return (
        <div className="flex min-h-screen">
            <Sidebar />
            <main className="flex-1 p-14">
                <div className="flex justify-between items-center mb-15">
                    <h2 className="text-xl font-bold px-7">Lista de Usuários</h2>
                </div>

                <div className="max-w-8xl p-6 bg-white rounded-lg shadow-md">
                    <div className="overflow-x-auto">
                        <div
                            style={{
                                maxHeight: '500px', 
                                overflowY: 'auto',
                                display: 'block',
                            }}
                        >
                            <table className="w-full bg-white shadow-md rounded-lg table-fixed">
                                <thead className="bg-gray-100">
                                    <tr>
                                        <th className="px-4 py-2 text-left w-1/6">Nome</th>
                                        <th className="px-4 py-2 text-left w-1/6">Idade</th>
                                        <th className="px-4 py-2 text-left w-1/6">Deletar</th>
                                        <th className="px-4 py-2 text-left w-1/6">Transações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {Array.isArray(users) && users.map(user => (
                                        <tr key={user.id} className="hover:bg-gray-100 border-b border-gray-200">
                                            <td className="px-4 py-2">{user.name}</td>
                                            <td className="px-4 py-2">{user.age} anos</td>
                                            <td className="px-8 py-2 text-center">
                                                <FaTrash
                                                    className="cursor-pointer hover:text-red-800 transition-colors"
                                                    onClick={() => openDeleteModal(user.id)}
                                                />
                                            </td>
                                            <td className="px-12 py-2 text-center">
                                                <FaEye className="cursor-pointer hover:text-gray-500 transition-colors"
                                                    onClick={() => openTransactionsModal(user.id)} />
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <DeleteModal
                    isOpen={!!selectedUser}
                    onClose={closeDeleteModal}
                    onConfirm={confirmDelete}
                />

                <TransactionsModal
                    isOpen={transactionsModalOpen}
                    onClose={closeTransactionsModal}
                    transactions={userTransactions}
                />

                <Footer />
            </main>
        </div>
    );
};

export default Users;
