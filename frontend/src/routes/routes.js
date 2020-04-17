import Home from "../components/routes/Home";
import Login from "../components/routes/Login";
import Clients from "../components/routes/Clients";
import Client from "../components/routes/Client";
import Statistics from "../components/routes/Statistics";
import Schedule from "../components/routes/Schedule";

export const routes = [
    { path: '/', component: Home },
    { path: '/statistics', component: Statistics },
    { path: '/home', component: Home },
    { path: '/schedule', component: Schedule },
    { path: '/login', component: Login, name: 'login'},
    { path: '/clients', component: Clients},
    { path: '/client/:id', component: Client},
    { path: '/client', component: Client},
];

