import axios from 'axios';

export const HTTP_OK = 200;
export const HTTP_CREATED = 201;
export const HTTP_ACCEPTED = 202;
export const HTTP_BAD_REQUEST = 400;
export const HTTP_FORBIDDEN = 403;
export const HTTP_NOT_FOUND = 404;
export const HTTP_TIMEOUT = 408;
export const HTTP_UNPROCESSABLE_ENTITY = 422;


axios.interceptors.request.use(config => {
    if (!!localStorage.getItem('token')) {
        config.headers.authorization= localStorage.getItem('token');
    }
    return config;
}, error => {
    return Promise.reject(error);
});

axios.interceptors.response.use(response => {
    return response;
}, error => {

    if(!!error.response.status) {
        switch(error.response.status) {
            case HTTP_TIMEOUT: {
                break
            }
            case HTTP_UNPROCESSABLE_ENTITY: {
                break
            }
            case HTTP_BAD_REQUEST: {
                break;
            }
            case HTTP_NOT_FOUND: {
                break;
            }
            default: {
                break
            }
        }
    }

    return Promise.reject(error)
});