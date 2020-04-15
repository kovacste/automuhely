import { queryString } from '../utils/HttpUtils';
import axios from 'axios';


export class Service {

    constructor(baseUrl) {
        this.baseUrl = baseUrl;

        if (this.constructor === Service) {
            throw 'Absztrak osztály, nem példányosítható!';
        }
    }

    getBaseUrl() {
        return this.baseUrl;
    }

    get(url, params = null, bodyParams = null) {
        return axios.get(this.getBaseUrl() + url + queryString(params), bodyParams ? { data: bodyParams } : {});
    }

    post(url, params) {
        return axios.post(this.getBaseUrl() + url, params);
    }

    head(url, params) {
        return axios.head(this.getBaseUrl() + url + queryString(params));
    }

    open(url, params) {
        window.open(this.getBaseUrl() + url + queryString(params));
    }
}