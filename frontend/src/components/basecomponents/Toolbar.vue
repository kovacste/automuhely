<template>

    <v-card color="grey lighten-4" flat tile width="100%">

        <v-toolbar dense>


            <v-tooltip left>

                <template v-slot:activator="{on}">

                    <v-icon v-on="on" class="back-icon" @click="$router.back()"> mdi-keyboard-backspace </v-icon>

                </template>

                <span> Vissza  </span>

            </v-tooltip>

            <v-divider class="mx-4" vertical />

            <v-toolbar-title class="toolbar-title">

                {{ title }}

            </v-toolbar-title>

            <v-divider class="mx-4" vertical />


            <v-tooltip left v-for="(fn, index) in functions" :key="index">

                <template v-slot:activator="{on}">

                    <v-icon class="ma-2" v-on="on"  @click="fn.cb()" :key="index"> {{ fn.icon }}</v-icon>

                </template>

                <span> {{ fn.tooltip }}</span>

            </v-tooltip>


            <v-spacer />

            <v-divider class="mx-4" vertical />

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
            },
            functions: {
                type: Array,
                required: false
            }
        },
        methods: {
            logout() {
                this.$store.commit('logOutUser');
                this.$router.push('/logout');
                localStorage.clear();
            }
        }
    }
</script>

<style scoped>
    .logout-text-button {
        cursor: pointer;
        font-size: 12px;
    }
    .back-icon {
        margin-left: 15px;
    }
</style>