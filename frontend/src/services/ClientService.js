import { Service } from "./Service";

class ClientService extends Service {

    getClient(id) {
        return this.get('GetClient?',{ clientId: id })
    }

    getClients() {
        return this.get('GetClientList');
    }

    getCities() {
        return this.get('GetCities');
    }

    getStreetTypes() {
        return this.get('GetStreetTypes');
    }

    setClient(client) {
        return this.post('SetClient', client);
    }

    removeClient(client) {
        return this.post('RemoveClient', client)
    }

}

export const clientService = new ClientService('/api/api/Client/');