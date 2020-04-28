<template>
    <PageBase title="Ügyfél adatok" width="8">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Ügyfél adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                <v-form>

                    <FormSubTitle :active="fieldsEditable" title="Személyes adatok" />

                    <v-layout row class="pa-1 ma-2">

                        <v-flex md1 xs1 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="client.id"
                                    label="Azonosító"
                                    name="id"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.nev"
                                    label="Név"
                                    name="nev"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!adoszamEditable"
                                    v-model="client.adoszam"
                                    label="Adószám"
                                    name="adoszam"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                    </v-layout>

                    <FormSubTitle :active="fieldsEditable" title="Cím" />

                    <v-layout row class="pa-1 ma-2">

                        <v-flex md1 xs6 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.irszam"
                                    label="Irányítószám"
                                    name="irszam"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                                    @change="zipChange()"
                            />

                        </v-flex>

                        <v-flex md3 xs6 class="ma-2">

                            <v-select
                                    :disabled="!fieldsEditable || !(!!client.irszam && client.irszam.length === 4)"
                                    v-model="client.telepulesid"
                                    label="Település"
                                    name="telepulesid"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                                    :items="filteredCities"
                                    item-text="nev"
                                    item-value="id"
                            />

                        </v-flex>

                        <v-flex v-if="false">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.telepules"
                                    label="Település"
                                    name="telepules"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs6 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.kozteruletneve"
                                    label="Közterület neve"
                                    name="kozteruletneve"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md2 xs6 class="ma-2">

                            <v-select
                                    :disabled="!fieldsEditable"
                                    v-model="client.kozteruletjellegid"
                                    label="Közterület jellege"
                                    name="kozteruletjellegid"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                                    :items="streetTypes"
                                    item-text="nev"
                                    item-value="id"
                            />

                        </v-flex>

                        <v-flex v-if="false">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.kozteruletjelleg"
                                    label="Közterület jellege"
                                    name="kozteruletjelleg"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md2 xs6 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.hazszam"
                                    label="Házszám"
                                    name="hazszam"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                    </v-layout>

                    <FormSubTitle :active="fieldsEditable" title="Elérhetőségek" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.telefonszam"
                                    label="Telefonszám"
                                    name="telefonszam"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="client.email"
                                    label="E-mail cím"
                                    name="email"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                    </v-layout>

                    <FormSubTitle :active="false" title="Egyéb" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="client.rogzitve"
                                    label="Rögzítve"
                                    name="rogzitve"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="client.rogzitette"
                                    label="Rögzítette"
                                    name="rogzitette"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                    </v-layout>

                </v-form>

            </v-card-text>

            <v-card-actions>

                <v-spacer />

                <v-btn v-if="!fieldsEditable" @click="edit()" color="primary">Módosítás </v-btn>

                <v-btn v-if="fieldsEditable" @click="cancel()" color="primary">Mégse </v-btn>

                <v-btn v-if="fieldsEditable" @click="save()" color="primary">Mentés </v-btn>

            </v-card-actions>

        </v-card>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import {clientService} from "../../services/ClientService";
    import FormSubTitle from "../basecomponents/FormSubTitle";
    export default {
        name: "Client",
        components: { FormSubTitle, PageBase },
        data() {
            return {
                editable: false,
                newClient: false,
                cities: [],
                filteredCities: [],
                streetTypes: [],
                client: {
                   id: null,
                   nev: null,
                   adoszam: null,
                   irszam: null,
                   telepulesid: null,
                   telepules: null,
                   kozteruletneve: null,
                   kozteruletjellegid: null,
                   kozteruletjelleg: null,
                   hazszam: null,
                   telefonszam: null,
                   email: null,
                   rogzitve: null,
                   rogzitette: null,
                }
            }
        },
        mounted() {
            const id = parseInt(this.$route.params.id, 10);
            const client = this.$store.getters.client;
            this.newClient = !id;
            if(id && id === client.id) {
                this.client = { ...client };
            }


            const cachedCities = this.$store.getters.lists.cities;
            if(!cachedCities) {
                clientService.getCities().then(response => {
                    this.cities = response.data;
                    this.$store.commit('addList', { key: 'cities', list: this.cities })
                    this.zipChange();
                });
            } else {
                this.cities = cachedCities;
                this.zipChange();
            }

            const cachedStreetTypes = this.$store.getters.lists.streetTypes;
            if(!cachedStreetTypes) {
                clientService.getStreetTypes().then(response => {
                    this.streetTypes = response.data;
                    this.$store.commit('addList', { key: 'streetTypes', list: this.streetTypes })
                });
            } else {
                this.streetTypes = cachedStreetTypes;
            }

        },
        computed: {
            adoszamEditable() {
                return !this.client.id && this.fieldsEditable;
            },
            fieldsEditable() {
                return this.editable || this.newClient;
            }
        },
        methods: {
            edit() {
                this.editable = true;
            },
            cancel() {
                this.editable = false;
            },
            save() {
                this.editable = false;
                if(this.newClient) {
                    this.client.id = 0;
                    this.client.rogzitette = this.$store.getters.user.username;
                    this.client.rogzitve = new Date().toISOString();
                }

                this.$store.commit('setClient', this.client);
                this.$store.dispatch('saveClient').then(() => {
                     this.$toasted.show('Ügyfél mentése sikeres!',{ 
                        theme: "toasted-primary", 
                        position: "bottom-right", 
                        duration : 5000
                    });
                });
            },
            zipChange() {
                this.filteredCities = this.cities.filter(city => {
                    return city.irszam === this.client.irszam;
                });
            }
        }
    }
</script>