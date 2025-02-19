import { useRoutes } from "react-router-dom";
import routes from "../Routes/Routes";

//Componente responsável por armazenar todas as rotas da aplicação
const Router = () => {
    return useRoutes(routes);
};

export default Router;
