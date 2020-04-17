import Vuex from "vuex";
import { user } from "./user/user";
import Vue from "vue";
import { client } from "./client/client";
Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: [user, client],
    state: {},
    getters: {},
    mutations: {},
    actions: {}
});