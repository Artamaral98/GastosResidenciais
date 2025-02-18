import React from "react";
import Sidebar from "../Components/Sidebar";
import useUsers from "../Services/Hooks/useUsers";
import useFocus from "../Services/Hooks/useFocus";
import Footer from "../Components/Footer"

const Home: React.FC = () => {
    const { newUser, handleChange, handleSubmit } = useUsers();
    const { inputRef } = useFocus();

    return (
        <div className="flex min-h-screen">
            <Sidebar />
            <main className="flex-1 p-14">
                <div className="flex justify-between items-center mb-15">
                    <h2 className="text-xl font-bold px-7">Novo Usuário</h2>
                </div>

                <div className="max-w-5xl h-180 p-6 bg-white rounded-lg shadow-md">
                    <form className="space-y-6" onSubmit={handleSubmit}>
                        <div className="grid grid-cols-3 gap-6">
                            <div className="col-span-2">
                                <label htmlFor="name" className="block text-sm mb-2">
                                    Nome
                                </label>
                                <input
                                    ref={inputRef}
                                    type="text"
                                    id="name"
                                    maxLength={30}
                                    value={newUser.name}
                                    onChange={handleChange}
                                    className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25] focus:border-transparent"
                                />
                            </div>
                            <div>
                                <label htmlFor="age" className="block text-sm mb-2">
                                    Idade
                                </label>
                                <input
                                    type="number"
                                    id="age"
                                    max={100}
                                    value={newUser.age}
                                    onChange={handleChange}
                                    className="w-full px-3 py-2 bg-gray-100 border-none rounded-md focus:outline-none focus:ring-2 focus:ring-[#DD4B25] focus:border-transparent"
                                />
                            </div>
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
    );
};

export default Home;
