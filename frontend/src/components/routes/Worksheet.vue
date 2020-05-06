<template>
    <PageBase title="Alkatrész adatok" width="8">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Munkalap adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                <v-form v-model="formValid">

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
                                    v-mask="'####-##-##'"
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

                                <span> Ügyfél hozzárendelés </span>

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

                        <v-flex md1 xs12 class="ma-2" v-if="fieldsEditable && this.worksheet.id > 0">

                            <v-tooltip left>

                                <template v-slot:activator="{on}">

                                    <v-icon v-on="on" x-large @click="openOrderWindow()"> mdi-hammer-wrench </v-icon>

                                </template>

                                <span> Alkatrész rendelés </span>

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

                                    <v-layout>

                                        <v-flex md6>

                                            {{ service.szolgaltatas.nev }}

                                        </v-flex>

                                        <v-flex md1>

                                            <v-icon v-if="fieldsEditable" @click="removeService(service.id, index)"> mdi-close </v-icon>

                                        </v-flex>

                                    </v-layout>


                                </li>

                            </ul>


                        </v-flex>

                    </v-layout>

                    <FormSubTitle :active="false" title="Munkához rendelt alkatrészek" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <ul>

                                <li v-for="(order, index) in orderedParts" :key="index">

                                    <v-layout>

                                        <v-flex md6>

                                            {{ order.alkatresz.nev }}

                                        </v-flex>

                                        <v-flex md1>

                                            <v-icon v-if="fieldsEditable" @click="removeOrder(order.id, index)"> mdi-close </v-icon>

                                        </v-flex>

                                    </v-layout>

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
                            />

                        </v-flex>

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="worksheet.rogzitette"
                                    label="Rögzítette"
                                    name="rogzitette"
                                    type="text"
                            />

                        </v-flex>

                    </v-layout>

                </v-form>

            </v-card-text>

            <v-card-actions>

                <v-spacer />

                <v-btn v-if="!fieldsEditable && !worksheet.lezarva" @click="edit()" color="primary">Módosítás </v-btn>

                <v-btn v-if="fieldsEditable && !newWorksheet" @click="cancel()" color="primary">Mégse </v-btn>

                <v-btn v-if="fieldsEditable && formValid" @click="save()" color="primary">Mentés </v-btn>

                <v-btn v-if="!fieldsEditable  && !worksheet.lezarva && canClose" @click="closeWorksheet()" color="primary">Munkalap lezárása </v-btn>

            </v-card-actions>

            <v-dialog v-model="clientSelectDialog" width="700">

                <v-card>

                    <v-card-title class="headline grey lighten-2" primary-title>

                        Ügyfél kiválasztása

                    </v-card-title>

                    <v-card-text>

                        <h3 class="subtitle-1 ma-2"> Válassza ki a listából a munkalaphoz rendelni kívánt ügyfelet. </h3>

                        <table>

                            <tr>

                                <td> Ügyél neve</td>

                                <td> Ügyél Adószáma </td>

                            </tr>

                            <tr v-for="(client, index) in clients" :key="index">

                                <td> {{ client.nev }} </td>

                                <td> {{ client.adoszam }} </td>

                                <td>

                                    <v-btn text @click="loadClient(client)"> Betöltés </v-btn>

                                </td>

                            </tr>

                        </table>


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

                        <h3 class="subtitle-1 ma-2">  Válassza ki a listából a hozzá adni kívánt szolgáltatást. </h3>

                        <table>

                            <tr v-for="(service, index) in services" :key="index">

                                <td> {{ service.nev }}  </td>

                                <td>

                                   <v-btn text @click="loadService(service)"> Hozzáad </v-btn>

                                </td>

                            </tr>

                        </table>

                    </v-card-text>

                    <v-divider />

                    <v-card-actions>

                        <v-spacer />

                        <v-btn
                                color="primary"
                                text
                                @click="servicesDialog = false"
                        >

                            Rendben

                        </v-btn>

                    </v-card-actions>

                </v-card>

            </v-dialog>

            <v-dialog v-model="partsDialog" width="700">

                <v-card>

                    <v-card-title class="headline grey lighten-2" primary-title>

                        Alkatrészek

                    </v-card-title>

                    <v-card-text>

                        <h3 class="subtitle-1 ma-2">  Válassza ki a megrendelni kívánt alkatrészt </h3>

                        <table>

                            <tr  v-for="(part, index) in parts" :key="index">

                                <td> {{ part.nev }}  </td>

                                <td>

                                    <v-btn text @click="orderPart(part)"> Megrendel </v-btn>

                                </td>

                            </tr>

                        </table>

                    </v-card-text>

                    <v-divider />

                    <v-card-actions>

                        <v-spacer />

                        <v-btn
                                color="primary"
                                text
                                @click="partsDialog = false"
                        >

                            Rendben

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
    import {partService} from "../../services/PartService";
    import {toast} from "../../mixins/toast";
    import {CLIENT, SERVICE} from "../../User";
    export default {
        name: "Worksheet",
        components: { FormSubTitle, PageBase },
        mixins: [toast],
        data() {
            return {
                formValid: false,
                editable: false,
                newWorksheet: false,
                clientSelectDialog: false,
                servicesDialog: false,
                partsDialog: false,
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
                services: [],
                parts: [],
                orderedParts: []
            }
        },
        mounted() {
            const id = parseInt(this.$route.params.id, 10);
            this.newWorksheet = !id;

            if(!this.newWorksheet) {
                const worksheet = this.$store.getters.worksheet;

                new Promise((resolve) => {
                    if(worksheet.id && id === worksheet.id) {
                        this.worksheet = { ...worksheet };
                        resolve();
                    } else {
                        worksheetService.getWorkSheet(id).then(response => {
                            this.worksheet = response.data;
                            resolve();
                        })
                    }
                }).then(() => {
                    worksheetService.getWorksheetDetails(id).then(details => {
                        this.worksheet.tetelek = details.data;
                        this.$store.commit('setWorksheet', this.worksheet)
                    });
                    worksheetService.getWorksheetOrder(id).then(orders => {
                        this.orderedParts = orders.data;
                    });
                })

            }
        },
        computed: {
            fieldsEditable() {
                return (this.editable || this.newWorksheet) && !this.worksheet.lezarva;
            },
            worksheetStatus() {
                return this.worksheet.lezarva ? 'Munkalap lezárt' : 'Munkalap nyitott';
            },
            canClose() {
                return this.$store.getters.user.modules.includes(SERVICE)
            }
        },
        methods: {
            orderPart(part) {
                const partToOrder = {
                    id: 0,
                    munkalapid: this.worksheet.id,
                    alkatresz: part,
                    mennyiseg: 1,
                    rogzitette: this.$store.getters.user.username
                };
                worksheetService.setWorksheetOrder([partToOrder]).then(() => {
                    this.orderedParts.push(partToOrder)
                }).catch(() => {
                    this.saveFail('Rendelés rögzítése sikertelen!');
                })
            },
            removeService(serviceid, index) {
                worksheetService.removeWorkSheetDetail(serviceid).then(() => {
                    this.worksheet.tetelek.splice(index, 1);
                }).catch(() => {
                    this.saveFail('Szolgáltatás eltávolítása sikertelen!');
                })
            },
            removeOrder(orderid, index) {
                worksheetService.removeWorksheetOrder(orderid).then(() => {
                    this.orderedParts.splice(index, 1);
                }).catch(() => {
                    this.saveFail('Rendelés eltávolítása sikertelen!')
                })
            },
            closeWorksheet() {
                this.worksheet.lezarta = this.$store.getters.user.username;
                worksheetService.closeWorksheet(this.worksheet.id, this.worksheet.lezarta).then(() => {
                    this.saveSuccess();
                    this.worksheet.lezarva = true;
                }).catch(() => {
                    this.saveFail('Munkalap lezárása sikertelen!');
                });
                this.editable = false;
            },
            loadClient(client) {
                this.worksheet.ugyfel = client;
                this.clientSelectDialog = false;
            },
            loadService(service){
                if(this.worksheet.tetelek == null) this.worksheet.tetelek = [];
                this.worksheet.tetelek.push({
                    id: 0,
                    munkalapid: this.worksheet.id ? this.worksheet.id : 0,
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
            openOrderWindow() {
                if(this.parts.length === 0) {
                    partService.getPartList().then(response => {
                        this.parts = response.data;
                        this.partsDialog = true
                    });
                } else {
                    this.partsDialog = true;
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
                }
                if(this.$store.getters.user.modules.includes(CLIENT)) {
                    this.worksheet.rogzitette = 'ugyfel';
                } else {
                    this.worksheet.rogzitette = this.$store.getters.user.username;
                }
                this.worksheet.rogzitve = new Date().toISOString();
                this.$store.commit('setWorksheet', this.worksheet);
                this.$store.dispatch('saveWorksheet').then((response) => {
                     this.saveSuccess();
                     if(this.newWorksheet) {
                         this.worksheet.id = response.data;
                         this.newWorksheet = false;
                     }
                }).catch(() => {
                    this.saveFail();
                });
            }
        }
    }
</script>