import React from "react";
import { Link } from "react-router-dom";

const Sidebar: React.FC = () => {
    return (
        <div className="w-[300px] bg-[#DD4B25] text-white min-h-screen">
            <div className="p-12">
                <Link to='/' className="text-4xl font-bold">Gastos</Link>
                <p className="text-xs px-2">Controle de gastos</p>
            </div>

            <div className="p-4 hover:bg-[#F76A2C] transition-colors">
                <Link to='/' className="px-6">Novo Usuário</Link>
            </div>
            <div className="p-4 hover:bg-[#F76A2C] transition-colors">
                <Link to="/usuarios" className='px-6'>Usuários</Link>
            </div>
            <div className="p-4 hover:bg-[#F76A2C] transition-colors">
                <Link to="/nova-transacao" className='px-6'>Nova Transação</Link>
            </div>
            <div className="p-4 hover:bg-[#F76A2C] transition-colors">
                <Link to="/transacoes" className='px-6'>Transações</Link>
            </div>
            <div className="p-4 hover:bg-[#F76A2C] transition-colors">
                <Link to="/consulta-de-totais" className='px-6'>Consulta de Totais</Link>
            </div>
        </div>
    )
}

export default Sidebar;
