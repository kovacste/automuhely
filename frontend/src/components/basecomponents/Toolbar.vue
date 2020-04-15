<template>

    <v-card color="grey lighten-4" flat tile width="100%">
        <v-toolbar dense>

            <v-toolbar-title class="toolbar-title">

                {{ title }}

            </v-toolbar-title>

            <v-spacer />

            <span class="logout-text-button" @click="logout()">

              <v-icon> mdi-logout </v-icon>

              Kijelentkezés

            </span>

        </v-toolbar>

     </v-card>

</template>

<script>
    export default {
        name: "Toolbar",
        props: {
            title: {
                type: String,
                required: true
            }
        },
        methods: {
            logout() {
                this.$store.commit('logOutUser');
                this.$router.push('/login');
            }
        }
    }
</script>

<style scoped>
    .logout-text-button {
        cursor: pointer;
        font-size: 12px;
    }
    .toolbar-title {
        margin-left: 20px;
    }
</style>