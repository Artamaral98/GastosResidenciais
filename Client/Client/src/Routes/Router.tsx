import { useRoutes, RouteObject } from "react-router-dom";
import routes from "../Routes/Routes";

//Componente respons�vel por armazenar todas as rotas da aplica��o

const Router = () => {
    const allRoutes: RouteObject[] = [...routes];
    const routing = useRoutes(allRoutes);
    return routing;
}

export default Router;
