import {partService} from "../../services/PartService";

export const part = {
    state: {
        part: {}
    },

    getters: {
        part: state => state.part
    },

    mutations: {
        setPart: (state, part) => state.part = part
    },

    actions: {
        saveService(context) {
            return partService.setPart(context.state.part)
        }
    }
};