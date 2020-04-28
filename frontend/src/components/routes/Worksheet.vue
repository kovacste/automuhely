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

                        <v-flex md1 xs12 class="ma-2" v-if="fieldsEditable">

                            <v-tooltip left>

                                <template v-slot:activator="{on}">

                                    <v-icon v-on="on" x-large @click="openClientSelectWindow()"> mdi-account-group </v-icon>

                                </template>

                                <span> Ügyfél hozzárednelés </span>

                            </v-tooltip>

                        </v-flex>

                        <v-flex md1 xs12 class="ma-2" v-if="fieldsEditable">

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

                                    {{ service.szolgaltatas.nev }} <v-icon v-if="fieldsEditable" @click="removeService(service.id, index)"> mdi-close </v-icon>

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

                <v-btn v-if="!fieldsEditable && !worksheet.lezarva" @click="edit()" color="primary">Módosítás </v-btn>

                <v-btn v-if="fieldsEditable" @click="cancel()" color="primary">Mégse </v-btn>

                <v-btn v-if="fieldsEditable" @click="save()" color="primary">Mentés </v-btn>

                <v-btn v-if="!fieldsEditable  && !worksheet.lezarva" @click="closeWorksheet()" color="primary">Munkalap lezárása </v-btn>

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
    import {worksheetService} from "../../services/WorksheetService";
    export default {
        name: "Worksheet",
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

                worksheetService.getWorksheetDetails(id).then(response => {
                    this.worksheet.tetelek = response.data;
                })
            }
        },
        computed: {
            fieldsEditable() {
                return (this.editable || this.newWorksheet) && !this.worksheet.lezarva;
            },
            worksheetStatus() {
                return this.worksheet.lezarva ? 'Munkalap lezárt' : 'Munkalap nyitott';
            }
        },
        methods: {
            removeService(serviceid, index) {
                worksheetService.removeWorkSheetDetail(serviceid).then(() => {
                    this.worksheet.tetelek.splice(index, 1);
                })
            },
            closeWorksheet() {
                this.worksheet.lezarva = new Date().toISOString();
                this.worksheet.lezarta = this.$store.getters.user.username;
                this.save();
            },
            loadClient(client) {
                this.worksheet.ugyfel = client;
                this.clientSelectDialog = false;
            },
            loadService(service){
                if(this.worksheet.tetelek == null) this.worksheet.tetelek = [];
                this.worksheet.tetelek.push({
                    id: 0,
                    munkalapid: this.worksheet.id,
                    szolgaltatas: service,
                    mennyiseg: 1,
                    ar: service.egysegar,
                    rogzitve: new Date().toISOString(),
                    rogzitette: this.$store.getters.user.username,
                });


                if(this.worksheet.id > 0) {
                    worksheetService.setWorksheetDetail([{
                        id: 0,
                        munkalapid: this.worksheet.id,
                        szolgaltatas: service,
                        mennyiseg: 1,
                        ar: service.egysegar,
                        rogzitve: new Date().toISOString(),
                        rogzitette: this.$store.getters.user.username,
                    }]);
                }
            },
            openClientSelectWindow() {
                if(this.clients.length === 0) {
                    clientService.getClients().then(response => {
                        this.clients = response.data;
                        this.clientSelectDialog = true;
                    });
                } else {
                    this.clientSelectDialog = true;
                }
            },
            openServiceSelectWindow() {
                if(this.services.length === 0) {
                    servicesService.getServiceList().then(response => {
                        this.services = response.data;
                        this.servicesDialog = true;
                    });
                } else {
                    this.servicesDialog = true;
                }
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
                    this.worksheet.rogzitette = this.$store.getters.user.username;
                    this.worksheet.rogzitve = new Date().toISOString();
                }

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