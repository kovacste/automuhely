import Vuex from "vuex";
import { user } from "./user/user";
import Vue from "vue";
Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: [user],
    state: {},
    getters: {},
    mutations: {},
    actions: {}
});