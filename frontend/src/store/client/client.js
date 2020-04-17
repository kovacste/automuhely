import {clientService} from "../../services/ClientService";

export const client = {
    state: {
       client: {
            id: null,
            nev:  null,
            adoszam: null,
            telepulesid: null,
            irszam: null,
            telepules: null,
            kozteruletneve: null,
            kozteruletjellegid: null,
            kozteruletjelleg: null,
            hazszam: null,
            telefonszam: null,
            email: null,
            rogzitve: null,
            rogzitette: null,
       }
    },

    getters: {
        client: state => state.client
    },

    mutations: {
        setClient: (state, client) => state.client = client
    },

    actions: {
        saveClient(context) {
            return clientService.setClient(context.state.client)
        }
    }
};