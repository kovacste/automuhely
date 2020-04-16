import { Service } from "./Service";

class LoginService extends Service {

    login(username, password) {
        console.log(username, password);
        /*return Promise.resolve({
            modul: ['Vezető', 'Iroda', 'Szervíz', 'Ügyfél'],
            username: 'Teszt Béla',
        });*/
       //http://localhost:50873/User/Authenticate?loginName=iroda&password=rossz
       return this.get('/User/Authenticate?', {
            loginName: username,
            password: password
        })
    }

}

export const loginService = new LoginService('/api');