import { Service } from "./Service";

class LoginService extends Service {

    login(username, password) {
       return this.get('User/Authenticate?', {
            loginName: username,
            password: password
        })
    }

    clientLogin(username, password) {
        return this.get('api/Client/Authenticate?', {
            loginName: username,
            password: password
        })
    }
}

export const loginService = new LoginService('/api/');