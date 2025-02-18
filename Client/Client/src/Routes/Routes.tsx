import { RouteObject } from "react-router-dom";
import Home from "../Views/Home";
import Users from "../Views/Users";
import NewTransaction from "../Views/NewTransaction";
import Transactions from "../Views/Transactions";
import Total from "../Views/Total";

const routes: RouteObject[] = [
    {
        path: '/',
        element: <Home />
    },
    {
        path: '/usuarios',
        element: <Users />
    },
    {
        path: '/nova-transacao',
        element: <NewTransaction />
    },
    {
        path: '/transacoes',
        element: <Transactions />
    },
    {
        path: '/consulta-de-totais',
        element: <Total />
    },

];

export default routes;
