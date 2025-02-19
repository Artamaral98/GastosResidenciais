import React from "react";
import Sidebar from "../Components/Sidebar";
import useUsers from "../Services/Hooks/useUsers";
import useTransactions from "../Services/Hooks/useTransactions";
import Footer from "../Components/Footer";
import { NumericFormat } from "react-number-format";

const NewTransaction: React.FC = () => {
    const { users } = useUsers();
    const {
        selectedUser,
        setSelectedUser,
        description,
        setDescription,
        transactionType,
        setTransactionType,
        value,
        setValue,
        handleSubmit
    } = useTransactions()

    return (
        <div className="flex min-h-screen">
            <Sidebar />
            <main className="flex-1 p-14">
                <div className="flex justify-between items-center mb-15">
                    <h2 className="text-xl font-bold px-7">Nova Transação</h2>
                </div>

                <div className="max-w-5xl h-180 p-6 bg-white rounded-lg shadow-md">
                    <form className="space-y-6" onSubmit={handleSubmit}>
                        <div className="grid grid-cols-3 gap-6">
                            <div className="col-span-2">
                                <label htmlFor="user" className="block text-sm mb-2">
                                    Usuário
                                </label>
                                <select
                                    id="user"
                                    value={selectedUser}
                                    onChange={(e) => setSelectedUser(e.target.value)}
                                    className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25] focus:border-transparent"
                                >
                                    <option value="">Selecione um usuário</option>
                                    {users.map((user) => (
                                        <option key={user.id} value={user.id}>
                                            {user.name}
                                        </option>
                                    ))}
                                </select>
                            </div>
                            <div>
                                <label htmlFor="tipo" className="block text-sm mb-2">
                                    Tipo
                                </label>
                                <select
                                    id="tipo"
                                    value={transactionType}
                                    onChange={(e) => setTransactionType(e.target.value)}
                                    className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25] focus:border-transparent"
                                >
                                    <option value="0">Despesa</option>
                                    <option value="1">Receita</option>
                                </select>
                            </div>
                        </div>

                        <div>
                            <label htmlFor="descricao" className="block text-sm mb-2">
                                Descrição
                            </label>
                            <input
                                type="text"
                                id="descricao"
                                maxLength={25}
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25] focus:border-transparent"
                            />
                        </div>

                        <div>
                            <label htmlFor="valor" className="block text-sm mb-2">
                                Valor
                            </label>
                            <NumericFormat
                                thousandSeparator="."
                                decimalSeparator=","
                                prefix="R$ "
                                id="valor"
                                maxLength={19}
                                value={value}
                                allowNegative={false}
                                onValueChange={(values) => setValue(values.floatValue || 0)}
                                className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25] focus:border-transparent"
                            />
                        </div>

                        <button
                            type="submit"
                            className="px-14 py-3 bg-[#DD4B25] text-white rounded-md hover:bg-[#C64422] transition-colors"
                        >
                            Salvar
                        </button>
                    </form>
                </div>

                <Footer />
            </main>
        </div>
    )
}

export default NewTransaction;
