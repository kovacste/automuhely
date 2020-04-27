import Vue from "vue";
import Vuex from "vuex";
import { user } from "./user/user";
import { client } from "./client/client";
import { service } from "./service/service";
import { part } from "./part/part";
import { worksheet } from "./worksheet/worksheet";
Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: [user, client, service, part, worksheet],
    state: {
        lists: []
    },
    getters: {
        lists: (state) => state.lists

    },
    mutations: {
        addList: (state, {key, list}) => state.lists[key] = list
    },
    actions: {}
});