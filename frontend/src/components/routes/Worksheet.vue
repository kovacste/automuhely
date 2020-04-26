<template>
    <PageBase title="Alkatrész adatok" width="8">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Munkalap adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                <v-form>

                    <FormSubTitle :active="fieldsEditable" title="Alapadatok" />

                    <v-layout row class="pa-1 ma-2">

                        <v-flex md1 xs1 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="worksheet.id"
                                    label="Azonosító"
                                    name="id"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="worksheet.idopont"
                                    label="Időpont"
                                    name="idopont"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="true"
                                    v-model="worksheet.ugyfel.nev"
                                    label="Ügyfél neve"
                                    name="nev"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md1 xs12 class="ma-2">

                            <v-tooltip left>

                                <template v-slot:activator="{on}">

                                    <v-icon v-on="on" x-large @click="openClientSelectWindow()"> mdi-account-group </v-icon>

                                </template>

                                <span> Ügyfél hozzárednelés </span>

                            </v-tooltip>

                        </v-flex>

                        <v-flex md1 xs12 class="ma-2">

                            <v-tooltip left>

                                <template v-slot:activator="{on}">

                                    <v-icon v-on="on" x-large @click="openServiceSelectWindow()"> mdi-car-arrow-left </v-icon>

                                </template>

                                <span> Szolgáltatás hozzárendelés </span>

                            </v-tooltip>

                        </v-flex>

                        <v-flex md2 xs12 class="ma-2">
                            {{ worksheetStatus }}
                        </v-flex>


                    </v-layout>

                    <FormSubTitle :active="false" title="Munkalaphoz rendelt tételek" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <ul>

                                <li v-for="(service, index) in worksheet.tetelek" :key="index">

                                    {{ service.nev }}

                                </li>

                            </ul>

                        </v-flex>

                    </v-layout>

                    <FormSubTitle :active="false" title="Egyéb" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="worksheet.rogzitve"
                                    label="Rögzítve"
                                    name="rogzitve"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="worksheet.rogzitette"
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

            <v-dialog v-model="clientSelectDialog" width="700">

                <v-card>

                    <v-card-title class="headline grey lighten-2" primary-title>

                        Ügyfél kiválasztása

                    </v-card-title>

                    <v-card-text>

                        Válassza ki a listából a munkalaphoz rendelni kívánt ügyfelet.

                        <ul>

                            <li v-for="(client, index) in clients" :key="index">
                                {{ client.nev }} {{ client.adoszam }} <v-btn text @click="loadClient(client)"> Betöltés </v-btn>
                            </li>

                        </ul>

                    </v-card-text>

                    <v-divider />

                    <v-card-actions>

                        <v-spacer />

                        <v-btn
                                color="primary"
                                text
                                @click="clientSelectDialog = false"
                        >

                            Mégse

                        </v-btn>

                    </v-card-actions>

                </v-card>

            </v-dialog>

            <v-dialog v-model="servicesDialog" width="700">

                <v-card>

                    <v-card-title class="headline grey lighten-2" primary-title>

                        Szolgáltatás

                    </v-card-title>

                    <v-card-text>

                        Válassza ki a listából a hozzá adni kívánt szolgáltatást.

                        <ul>

                            <li v-for="(service, index) in services" :key="index">

                                {{ service.nev }} <v-btn text @click="loadService(service)"> Hozzáad </v-btn>

                            </li>

                        </ul>

                    </v-card-text>

                    <v-divider />

                    <v-card-actions>

                        <v-spacer />

                        <v-btn
                                color="primary"
                                text
                                @click="servicesDialog = false"
                        >

                            Mégse

                        </v-btn>

                    </v-card-actions>

                </v-card>

            </v-dialog>

        </v-card>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import FormSubTitle from "../basecomponents/FormSubTitle";
    import {clientService} from "../../services/ClientService";
    import {servicesService} from "../../services/ServicesService";
    export default {
        name: "Client",
        components: { FormSubTitle, PageBase },
        data() {
            return {
                editable: false,
                newWorksheet: false,
                clientSelectDialog: false,
                servicesDialog: false,
                worksheet: {
                    id: null,
                    idopont: null,
                    lezarva: null,
                    lezarta: null,
                    tetelek: [],
                    rogzitve: null,
                    rogzitette: null,
                    ugyfel: {
                        nev: null
                    }
                },
                clients: [],
                services: []
            }
        },
        mounted() {
            const id = parseInt(this.$route.params.id, 10);
            const worksheet = this.$store.getters.worksheet;
            this.newWorksheet = !id;
            if(id && id === worksheet.id) {
                this.worksheet = { ...worksheet };
            }
        },
        computed: {
            fieldsEditable() {
                return (this.editable || this.newWorksheet) && !this.worksheet.lezarva;
            },
            worksheetStatus() {
                return this.worksheet.lezarva ? 'Munkalap lezárt' : 'Munkalap nyitott'
            }
        },
        methods: {
            loadClient(client) {
                this.worksheet.ugyfel = client;
                this.clientSelectDialog = false;
            },
            loadService(service){
                this.worksheet.tetelek.push(service);
            },
            openClientSelectWindow() {
                clientService.getClients().then(response => {
                    this.clients = response.data
                    this.clientSelectDialog = true;
                });
            },
            openServiceSelectWindow() {
                servicesService.getServiceList().then(response => {
                    this.services = response.data
                    this.servicesDialog = true;
                });
            },
            edit() {
                this.editable = true;
            },
            cancel() {
                this.editable = false;
            },
            save() {
                this.editable = false;
                if(this.newWorksheet) {
                    this.worksheet.id = 0;
                }
                this.worksheet.rogzitette = this.$store.getters.user.username;
                this.worksheet.rogzitve = new Date().toISOString();
                this.$store.commit('setWorksheet', this.worksheet);
                this.$store.dispatch('saveWorksheet').then(() => {
                     this.$toasted.show('Munkalap mentése sikeres!',{
                        theme: "toasted-primary", 
                        position: "bottom-right", 
                        duration : 5000
                    });
                });
            }
        }
    }
</script>