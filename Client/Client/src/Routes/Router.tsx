import { useRoutes } from "react-router-dom";
import routes from "../Routes/Routes";

//Componente respons�vel por armazenar todas as rotas da aplica��o
const Router = () => {
    return useRoutes(routes);
};

export default Router;
