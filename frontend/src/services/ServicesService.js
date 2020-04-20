import { Service } from "./Service";

class ServicesService extends Service {

    getServiceList() {
        return this.get('GetServiceList');
    }

    setService(service) {
        return this.post('SetService', service);
    }

}

export const servicesService = new ServicesService('/api/api/Service/');