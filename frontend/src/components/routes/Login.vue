<template>

    <v-layout row>

        <v-container class="fill-height" fluid>

            <v-row align="center" justify="center">

                <v-col cols="12" sm="8" md="4">

                    <v-card class="elevation-12">

                        <v-toolbar color="primary" dark flat>

                            <v-toolbar-title> Bejelentkezés  </v-toolbar-title>

                        </v-toolbar>

                        <v-card-text>

                            <v-form>

                                <v-text-field
                                        label="Felhasználónév"
                                        name="login"
                                        type="text"
                                        v-model="username"
                                />

                                <v-text-field
                                        id="password"
                                        label="Jelszó"
                                        name="password"
                                        type="password"
                                        v-model="password"
                                />
                                <v-checkbox
                                        id="user-login"
                                        label="Dolgozó bejelentkezés"
                                        name="userLogin"
                                        type="userLogin"
                                        v-model="userLogin"
                                />


                            </v-form>

                        </v-card-text>

                        <v-card-actions>

                            <v-spacer />

                            <v-btn @click="login()" color="primary">Bejelentkezés </v-btn>

                        </v-card-actions>

                    </v-card>

                </v-col>

            </v-row>

        </v-container>

    </v-layout>

</template>

<script>
    import {loginService} from "../../services/LoginService";
    import {toast} from "../../mixins/toast";

    export default {
        name: "Login",
        mixins: [toast],
        data(){
            return {
                username: null,
                password: null,
                userLogin: false
            }
        },
        methods: {
            login() {
                if(this.userLogin) {
                    this.uLogin();
                } else {
                    this.clientLogin();
                }
            },
            clientLogin() {
                loginService.clientLogin(this.username, this.password).then(response => {
                    localStorage.setItem('token', 'token-value');
                    this.$store.commit('setUser', {
                        username: response.data.loginNev,
                        name: response.data.nev,
                        modules: response.data.modul
                    });
                    console.log(response)
                    localStorage.setItem('username', response.data.loginNev);
                    localStorage.setItem('name', response.data.nev);
                    localStorage.setItem('module', response.data.modul.join('-'));
                    this.$router.push('/home');
                }).catch((error) => {
                    if(error.response.data === 'NOT_AUTHENTICATED') {
                        this.saveFail('Hibás felhasználónév vagy jelszó!');
                    } else {
                        this.saveFail('Bejelentkezés sikertelen!');
                    }
                })
            },
            uLogin() {
                loginService.login(this.username, this.password).then(response => {
                    localStorage.setItem('token', 'token-value');
                    this.$store.commit('setUser', {
                        username: response.data.loginNev,
                        name: response.data.nev,
                        modules: response.data.modul
                    });
                    console.log(response)
                    localStorage.setItem('username', response.data.loginNev);
                    localStorage.setItem('name', response.data.nev);
                    localStorage.setItem('module', response.data.modul.join('-'));
                    this.$router.push('/home');
                }).catch((error) => {
                    if(error.response.data === 'NOT_AUTHENTICATED') {
                        this.saveFail('Hibás felhasználónév vagy jelszó!');
                    } else {
                        this.saveFail('Bejelentkezés sikertelen!');
                    }
                })
            }
        }
    }
</script>