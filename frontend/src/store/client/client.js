import {clientService} from "../../services/ClientService";

export const client = {
    state: {
       client: {}
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