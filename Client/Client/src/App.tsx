import React, { useState } from 'react';
import './App.css';
import routes from "./Routes/Routes";
import Router from "./Routes/Router";
import { RouteObject } from 'react-router-dom';

function App() {
    const [allRoutes, setAllRoutes] = useState<RouteObject[]>([...routes]);

    //Utiliza as rotas contidas no componente Router
    return <Router allRoutes={allRoutes} />;
}

export default App;
