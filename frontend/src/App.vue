<template>

  <v-app>

    <v-card height="100%">

      <v-navigation-drawer
              absolute
              permanent
              left
              v-if="hasLoggedInUser"
      >
        <template v-slot:prepend>

          <v-list-item two-line>

            <v-list-item-avatar>

                <v-icon>mdi-account-circle</v-icon>

            </v-list-item-avatar>

            <v-list-item-content>

              <v-list-item-title>{{ username }}</v-list-item-title>

            </v-list-item-content>

            <v-list-item-subtitle v-if="hasLoggedInUser">Bejelentkezve</v-list-item-subtitle>

          </v-list-item>

        </template>

        <v-divider />

        <v-list dense v-for="(moduleName, index) in getUserModules" :key="index">

          <h4 class="subtitle-2"> {{ getModuleLabel(moduleName) }}</h4>

          <v-list-group v-for="(menu, index) in getUserMenu(moduleName)" :prepend-icon="menu.icon" :key="index">

              <template v-slot:activator>

                <v-list-item-content>

                  <v-list-item-title> {{ menu.label }}</v-list-item-title>

                </v-list-item-content>

              </template>

              <v-list-item v-for="(submenu, index) in menu.items" :key="index" :to="submenu.to">

                <v-list-item-title> {{ submenu.title }}</v-list-item-title>

              </v-list-item>

          </v-list-group>

        </v-list>

      </v-navigation-drawer>

      <v-content class="content">

        <router-view />

      </v-content>

    </v-card>

  </v-app>

</template>

<script>

  import { CLIENT, MANAGER, OFFICE, SERVICE } from "./User";

export default {
  name: 'App',
  computed: {
    hasLoggedInUser() {
      return !!this.$store.getters.user
    },
    username() {
      return this.hasLoggedInUser ? this.$store.getters.user.username : '';
    },
    getUserModules() {
      return this.$store.getters.user.modules;
    }
  },
  mounted() {
      if(localStorage.getItem('username')) {
        this.$store.commit('setUser', {
          username: localStorage.getItem('username'),
          name: localStorage.getItem('name'),
          modules: localStorage.getItem('module').split('-')
        });

        if(this.$router.currentRoute.name === 'login') {
          this.$router.push('home');
        }
      }
  },
  methods: {
    getUserMenu(modulName) {
      return this.menuMap[modulName];
    },
    getModuleLabel(modulName) {
      return this.modulLabel[modulName];
    }
  },
  data: () => ({
    modulLabel: {
      [MANAGER]: 'Vezető',
      [OFFICE]: 'Iroda',
      [SERVICE]: 'Szerelő',
      [CLIENT]: 'Ügyfél',

    },
    menuMap : {
      [MANAGER]: {
        settings: {
          label: 'Beállítások',
          icon: 'mdi-cog',
          items: [
            { title: 'Szolgáltatások', to: '/home' },
          ]
        },
        parts: {
          label: 'Alkatrészek',
          icon: 'mdi-hammer-wrench',
          items: [
            { title: 'Minden alkatrész', to: '/home' },
          ]
        },
        clients: {
          label: 'Ügyfelek kezelése',
          icon: 'mdi-account-group',
          items: [
            { title: 'Összesített nézet', to: '/clients' },
          ]
        },
        statistics: {
          label: 'Statisztika',
          icon: 'mdi-trending-up',
          items: [
            { title: 'Dolgozói statisztikák', to: '/statistics' },
          ]
        }
      },
      [OFFICE]: {
        worksheets: {
          label: 'Munkalapok',
          icon: 'mdi-file-document-edit-outline',
          items: [
            { title: 'Új munkalap', to: '/worksheet' },
            { title: 'Munkalapok', to: '/worksheets' },
          ]
        },
        clients: {
          label: 'Ügyfelek kezelése',
          icon: 'mdi-account-group',
          items: [
            { title: 'Új ügyfél', to: '/client' },
            { title: 'Összesített nézet', to: '/clients' },
          ]
        },
        settings: {
          label: 'Beállítások',
          icon: 'mdi-cog',
          items: [
            { title: 'Alkatrészek', to: '/parts' },
            { title: 'Szolgáltatások', to: '/sevices' },
            { title: 'Ügyfelek', to: '/clients' },
          ]
        },
        statistics: {
          label: 'Statisztika',
          icon: 'mdi-trending-up',
          items: [
            { title: 'Dolgozói statisztikák', to: '/statistics' },
          ]
        }
      },
      [SERVICE]: {
        parts: {
          label: 'Alkatrészek',
          icon: 'mdi-hammer-wrench',
          items: [
            { title: 'Alkatrész rendelése', to: '/order' },
          ]
        },
        worksheets: {
          label: 'Munkalapok',
          icon: 'mdi-file-document-edit-outline',
          items: [
            { title: 'Aktuális munkalapok', to: '/worksheets/open' },
            { title: 'Korábbi munkalapok', to: '/worksheets/closed' },
          ]
        }
      },
      [CLIENT]: {
        settings: {
          label: 'Beállítások',
          icon: 'mdi-hammer-wrench',
          items: [
            { title: 'Saját adatok', to: '/profile' },
          ]
        },
        services: {
          label: 'Szolgáltatások',
          icon: 'mdi-car-arrow-left',
          items: [
            { title: 'Új szolgáltatás', to: '/service' },
            { title: 'Saját szolgáltatások', to: '/services/active' },
            { title: 'Időpontok', to: '/schedule' },
          ]
        }
      }
    },
  }),
};
</script>

<style scoped>
  .content {
    margin-left: 256px;
  }
</style>
