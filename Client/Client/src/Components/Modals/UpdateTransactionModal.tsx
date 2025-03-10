import React, { useEffect } from "react";
import { UpdateTransactionModalProps } from "../../Services/Models/Types";
import useTransactions from "../../Services/Hooks/useTransactions";
import { NumericFormat } from "react-number-format";
import useFocus from "../../Services/Hooks/useFocus";

const TransactionUpdateModal: React.FC<UpdateTransactionModalProps> = ({
    isOpen,
    transaction,
    closeModal,
}) => {
    const { editTransaction, setEditTransaction, updateTransaction, handleChangeTransaction } = useTransactions();
    const { inputRef } = useFocus();

    //carrega o modal com as informações da transação
    useEffect(() => {
        if (transaction && transaction.id !== null) {
            setEditTransaction({
                id: transaction.id,
                description: transaction.description,
                type: transaction.type === "expense" ? 0 : 1, //Permite que o tipo permaneça o mesmo
                valor: transaction.valor,
                userId: transaction.userId
            })
        }
    }, [transaction, setEditTransaction]);


    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 backdrop-blur-sm z-50 flex justify-center items-center ">
            <div className="bg-white p-8 rounded-md shadow-lg w-96 h-90 border-2 border-gray-300 relative">
                <h2 className="text-xl font-bold mb-4">Editar Transação</h2>

                
                <input
                    type="text"
                    id="transactionDescription"
                    ref={inputRef}
                    maxLength={22}
                    value={editTransaction.description}
                    onChange={(e) =>
                        handleChangeTransaction(e)}
                    className="border p-2 w-full mb-2 rounded-md"
                />

                <NumericFormat
                    value={editTransaction.valor ?? ""}
                    onValueChange={(values) =>
                        setEditTransaction((prev) => ({ ...prev, valor: values.floatValue ?? 0 }))
                    }
                    thousandSeparator="."
                    decimalSeparator=","
                    prefix="R$ "
                    className="border p-2 w-full mb-2 rounded-md"
                    allowNegative={false}
                />

                <select
                    value={editTransaction.type ?? ""}
                    onChange={(e) => setEditTransaction((prev) => ({ ...prev, type: Number(e.target.value) }))}
                    className="border p-2 w-full mb-2 rounded-md"
                >
                    <option value={0}>Despesa</option>
                    <option value={1}>Receita</option>
                </select>

                <div className="flex justify-end mt-4">
                    <button
                        onClick={closeModal}
                        className="bg-gray-500 text-white px-4 py-2 rounded mr-2 hover:bg-gray-400"
                    > Cancelar
                    </button>

                    <button
                        onClick={async () => {
                            const success = await updateTransaction(editTransaction.id!);
                            if (success) {
                                closeModal();
                            }
                        }}
                        className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
                    > Confirmar
                    </button>
                </div>
            </div>
        </div>
    )
}

export default TransactionUpdateModal;
