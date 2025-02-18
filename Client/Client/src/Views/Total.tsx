import React from "react";
import Sidebar from "../Components/Sidebar";
import useUsers from "../Services/Hooks/useUsers";
import Footer from "../Components/Footer";
import { IntlProvider, FormattedNumber } from "react-intl";

const Total: React.FC = () => {
    const { users, globalTotal } = useUsers()

    return (
        <IntlProvider locale="pt-BR">
            <div className="flex min-h-screen">
                <Sidebar />
                <main className="flex-1 p-14">
                    <div className="flex justify-between items-center mb-15">
                        <h2 className="text-xl font-bold px-7">Saldo</h2>
                    </div>

                    <div className="max-w-8xl p-6 bg-white rounded-lg shadow-md">
                        <div className="overflow-x-auto">
                            <table className="w-full bg-white shadow-md rounded-lg table-fixed">
                                <thead className="bg-gray-100">
                                    <tr>
                                        <th className="py-3 px-4 text-left">Pessoa</th>
                                        <th className="py-3 px-4 text-left">Idade</th>
                                        <th className="py-3 px-4 text-left">Receitas</th>
                                        <th className="py-3 px-4 text-left">Despesas</th>
                                        <th className="py-3 px-4 text-left">Saldo</th>
                                    </tr>
                                </thead>
                            </table>

                            <div
                                style={{
                                    maxHeight: '500px',
                                    overflowY: 'auto',
                                    display: 'block'
                                }}
                            >
                                <table className="w-full bg-white shadow-md rounded-lg table-fixed">
                                    <tbody>
                                        {users.map((user) => (
                                            <tr
                                                key={user.id}
                                                className="hover:bg-gray-50 border-b border-gray-200"
                                            >
                                                <td className="py-3 px-4">{user.name}</td>
                                                <td className="py-3 px-4">{user.age}</td>
                                                <td className="py-3 px-4 text-blue-500">
                                                    R$ <FormattedNumber value={user.totalRevenue} minimumFractionDigits={2} />
                                                </td>
                                                <td className="py-3 px-4 text-red-500">
                                                    R$ <FormattedNumber value={user.totalExpenses} minimumFractionDigits={2} />
                                                </td>
                                                <td
                                                    className={`py-3 px-4 font-bold ${user.balance < 0
                                                        ? "text-red-500"
                                                        : "text-blue-500"
                                                        }`}
                                                >
                                                    R$ <FormattedNumber value={user.balance} minimumFractionDigits={2} />
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                            <table className="w-full bg-white shadow-md rounded-lg table-fixed">
                                <tfoot className="bg-gray-100">
                                    <tr>
                                        <td className="py-3 px-4 font-bold">Total Geral Líquido</td>
                                        <td className="py-3 px-4 font-bold">-</td>
                                        <td className="py-3 px-4 text-blue-500 font-bold">
                                            R$ <FormattedNumber value={globalTotal.totalRevenue} minimumFractionDigits={2} />
                                        </td>

                                        <td className="py-3 px-4 text-red-500 font-bold">
                                            R$ <FormattedNumber value={globalTotal.totalExpenses} minimumFractionDigits={2} />
                                        </td>

                                        <td
                                            className={`py-3 px-4 font-bold ${globalTotal.valor < 0
                                                ? "text-red-500"
                                                : "text-blue-500"
                                                }`}
                                        >
                                            R$ <FormattedNumber value={globalTotal.valor} minimumFractionDigits={2} />
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>

                    <Footer />
                </main>
            </div>
        </IntlProvider>
    )
}

export default Total;
