import Vuex from "vuex";
import { user } from "./user/user";
import Vue from "vue";
import { client } from "./client/client";
import {service} from "./service/service";
import {part} from "./part/part";
Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: [user, client, service, part],
    state: {},
    getters: {},
    mutations: {},
    actions: {}
});