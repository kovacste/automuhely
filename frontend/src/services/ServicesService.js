import { Service } from "./Service";

class ServicesService extends Service {

    getService(id) {
        return this.get('GetService?',  { serviceId: id })
    }

    getServiceList() {
        return this.get('GetServiceList');
    }

    setService(service) {
        return this.post('SetService', service);
    }

    removeService(service) {
        return this.post('RemoveService', service)
    }

}

export const servicesService = new ServicesService('/api/api/Service/');