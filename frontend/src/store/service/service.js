import { servicesService } from "../../services/ServicesService";

export const service = {
    state: {
        service: {}
    },

    getters: {
        service: state => state.service
    },

    mutations: {
        setService: (state, service) => state.service = service
    },

    actions: {
        saveService(context) {
            return servicesService.setService(context.state.service)
        }
    }
};