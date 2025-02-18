import { useRoutes, RouteObject } from "react-router-dom";
import routes from "../Routes/Routes";

//Componente responsável por armazenar todas as rotas da aplicação

const Router = () => {
    const allRoutes: RouteObject[] = [...routes];
    const routing = useRoutes(allRoutes);
    return routing;
}

export default Router;
