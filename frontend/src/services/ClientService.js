import { Service } from "./Service";

class ClientService extends Service {

    getClients() {
        //return this.get('/clients')
        return Promise.resolve({
            clients:  [
                {id: 1,  name: 'Kovács Béla', zip: '9700', city: 'Szombathely', taxnum: '1234532-3-34' },
                {id: 2,  name: 'Gipsz Jakab', zip: '1100', city: 'Vép', taxnum: '1234532-3-34' },
                {id: 3,  name: 'John Doe', zip: '9020', city: 'Doboz', taxnum: '1234532-3-34' },
                {id: 4,  name: 'Juan Pál', zip: '4500', city: 'Bugyi', taxnum: '1234532-3-34' },
                {id: 5,  name: 'Németh Poccahontas', zip: '1111', city: 'Hatvan', taxnum: '1234532-3-34' },
                {id: 6,  name: 'Rick Sanchez', zip: '6700', city: 'Hetven', taxnum: '1234532-3-34' },
                {id: 7,  name: 'Tony Montana', zip: '3500', city: 'Nyíregyháza', taxnum: '1234532-3-34' },
                {id: 8,  name: 'Antonio Banderas', zip: '8700', city: 'Ózd', taxnum: '1234532-3-34' },
            ]
        });
    }

}

export const clientService = new ClientService('/api');