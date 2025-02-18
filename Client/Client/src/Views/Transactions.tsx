import React from "react";
import Sidebar from "../Components/Sidebar";
import useTransactions from "../Services/Hooks/useTransactions";
import Footer from "../Components/Footer";
import formatTime from "../Utils/FormatTime";
import { FaTrash } from "react-icons/fa";
import DeleteTransactionModal from "../Components/DeleteTransactionModal";
import { IntlProvider, FormattedNumber } from "react-intl";

const Transactions: React.FC = () => {
    const {
        transactions,
        openDeleteTransactionModal,
        closeTransactionDeleteModal,
        selectedTransaction,
        confirmDeleteTransaction,
    } = useTransactions();

    return (
        <IntlProvider locale="pt-BR">
            <div className="flex min-h-screen">
                <Sidebar />
                <main className="flex-1 p-14">
                    <div className="flex justify-between items-center mb-15">
                        <h2 className="text-xl font-bold px-7">Transações</h2>
                    </div>

                    <div className="max-w-8xl p-7 bg-white rounded-lg shadow-md">
                        <div className="overflow-x-auto">
                            <table className="w-full bg-white shadow-md rounded-lg table-fixed">
                                <thead className="bg-gray-100">
                                    <tr>
                                        <th className="py-3 px-4 text-left w-1/6">Usuário</th>
                                        <th className="py-3 px-4 text-left w-1/6">Idade</th>
                                        <th className="py-3 px-4 text-left w-1/6">Descrição</th>
                                        <th className="py-3 px-4 text-left w-1/6">Valor</th>
                                        <th className="py-3 px-4 text-left w-1/6">Data</th>
                                        <th className="py-3 px-4 text-left w-1/6">Hora</th>
                                        <th className="py-3 px-4 text-left w-1/6"></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {Array.isArray(transactions) &&
                                        transactions.map((transaction) => (
                                            <tr
                                                key={transaction.id}
                                                className="hover:bg-gray-100 border-b border-gray-200"
                                            >
                                                <td className="py-3 px-4">{transaction.name}</td>
                                                <td className="py-3 px-4">{transaction.age}</td>
                                                <td className="py-3 px-4">{transaction.description}</td>
                                                <td
                                                    className={`py-3 px-4 font-bold ${transaction.types === "expense"
                                                            ? "text-red-500"
                                                            : "text-blue-500"
                                                        }`}
                                                >
                                                    {transaction.types === "expense" && "-"}
                                                    R${" "}
                                                    <FormattedNumber
                                                        value={transaction.valor}
                                                        minimumFractionDigits={2}
                                                        maximumFractionDigits={2}
                                                    />
                                                </td>
                                                <td className="py-3 px-4">
                                                    {new Date(transaction.createdAt).toLocaleDateString()}
                                                </td>
                                                <td className="py-3 px-4">
                                                    {formatTime(transaction.createdAt)}
                                                </td>
                                                <td className="py-3 px-4 text-center">
                                                    <FaTrash
                                                        className="cursor-pointer hover:text-red-800 transition-colors"
                                                        onClick={() =>
                                                            openDeleteTransactionModal(transaction.id)
                                                        }
                                                    />
                                                </td>
                                            </tr>
                                        ))}
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <DeleteTransactionModal
                        isOpen={!!selectedTransaction}
                        onClose={closeTransactionDeleteModal}
                        onConfirm={confirmDeleteTransaction}
                    />

                    <Footer />
                </main>
            </div>
        </IntlProvider>
    );
};

export default Transactions;
