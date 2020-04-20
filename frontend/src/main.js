import Vue from 'vue';
import App from './App.vue';
import VueRouter from 'vue-router';
import { routes } from "./routes/routes";
import vuetify from './plugins/vuetify';
import { store } from "./store/store";
import VueGoogleCharts from 'vue-google-charts';
import Toasted from 'vue-toasted';

Vue.config.productionTip = false;

Vue.use(VueGoogleCharts);
Vue.use(Toasted)
Vue.use(VueRouter);

const router = new VueRouter({ routes, mode: 'history' });

router.beforeEach((to, from, next) => {
  const loggedIn = store.getters.user !== null || localStorage.getItem('username')



  console.log('BE VAN JELENTKEZVE', loggedIn)

  if(loggedIn && to.name === 'login') {
    next('/home');
  }

  if(!loggedIn && to.name !== 'login') {
    next('/login');
  } else {
    next();
  }

});

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app');
