import { Service } from "./Service";

class ClientService extends Service {

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

}

export const clientService = new ClientService('/api/api/Client/');