/**
 * Paraméterben kapott objektum elemeiből készít query stringet
 * @param params
 * @returns {string} a querystring vagy üres string ha a függvényparaméter nem definiált
 */
export function queryString(params) {
    if(params === null || typeof params === 'undefined') return '';
    return Object.keys(params).map(k => encodeURIComponent(k) + '=' + encodeURIComponent(params[k])).join('&')
}