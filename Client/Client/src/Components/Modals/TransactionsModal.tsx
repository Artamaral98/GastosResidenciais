import React from "react";
import { FaTimes } from "react-icons/fa";
import { TransactionsModalProps } from "../../Services/Models/Types";
import formatTime from "../../Utils/FormatTime";
import { IntlProvider, FormattedNumber } from "react-intl";

const TransactionsModal: React.FC<TransactionsModalProps> = ({ isOpen, onClose, transactions }) => {
    if (!isOpen) return null

    return (
        <IntlProvider locale="pt-BR">
            <div className="fixed inset-0 flex justify-center items-center backdrop-blur-sm z-50">
                <div className="bg-white rounded-lg p-6 w-full max-w-4xl shadow-lg">
                    <div className="flex justify-between items-center mb-4">
                        <h2 className="text-xl font-bold">Transações</h2>
                        <FaTimes
                            className="cursor-pointer text-gray-600 hover:text-gray-800"
                            onClick={onClose}
                        />
                    </div>

                    <div className="overflow-y-auto max-h-96">
                        <table className="min-w-full table-auto">
                            <thead>
                                <tr>
                                    <th className="px-4 py-2 text-left">Descrição</th>
                                    <th className="px-4 py-2 text-left">Valor</th>
                                    <th className="px-4 py-2 text-left">Tipo</th>
                                    <th className="px-4 py-2 text-left">Data</th>
                                    <th className="px-4 py-2 text-left">Hora</th>
                                </tr>
                            </thead>
                            <tbody>
                                {transactions.map(transaction => (
                                    <tr key={transaction.id} className="hover:bg-gray-100">
                                        <td className="px-4 py-2">{transaction.description}</td>
                                        <td className={`px-4 py-2 ${transaction.types === "expense" ? 'text-red-600' : 'text-blue-600'}`}>
                                            {transaction.types === "expense" && "-"}
                                            R$ <FormattedNumber value={transaction.valor} minimumFractionDigits={2} />
                                        </td>
                                        <td className="px-4 py-2">
                                            {transaction.types === "expense" ? 'Despesa' : 'Receita'}
                                        </td>
                                        <td className="px-4 py-2">
                                            {new Date(transaction.createdAt).toLocaleDateString()}
                                        </td>
                                        <td className="px-4 py-2">
                                            {formatTime(transaction.createdAt)}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </IntlProvider>
    )
}

export default TransactionsModal;
