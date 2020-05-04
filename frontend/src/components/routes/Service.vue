<template>

    <PageBase title="Szolgáltatás adatok" width="8">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Szolgáltatás adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                <v-form v-model="formValid">

                    <FormSubTitle :active="fieldsEditable" title="Alapadatok" />

                    <v-layout row class="pa-1 ma-2">

                        <v-flex md1 xs1 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="service.id"
                                    label="Azonosító"
                                    name="id"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="service.nev"
                                    label="Szolgáltatás neve"
                                    name="nev"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="service.egysegar"
                                    label="Egységár"
                                    name="egysegar"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-select
                                    :disabled="!fieldsEditable"
                                    v-model="service.ismetlodo"
                                    label="Ismétlődő"
                                    name="ismetlodo"
                                    item-text="item-text"
                                    item-value="item-value"
                                    :items="[
                                        {'item-text': 'Igen', 'item-value': true},
                                        {'item-text': 'Nem', 'item-value': false},
                                    ]"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2" v-if="service.ismetlodo">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="service.ismetlodesiidoszak"
                                    label="Ismétlődés gyakorisága napokban"
                                    name="ismetlodesiidoszak"
                                    v-mask="'###'"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                    </v-layout>

                    <FormSubTitle :active="false" title="Egyéb" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="service.rogzitve"
                                    label="Rögzítve"
                                    name="rogzitve"
                                    type="text"
                            />

                        </v-flex>

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="service.rogzitette"
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

                <v-btn v-if="!fieldsEditable" @click="edit()" color="primary">Módosítás </v-btn>

                <v-btn v-if="fieldsEditable && !newService" @click="cancel()" color="primary">Mégse </v-btn>

                <v-btn v-if="fieldsEditable && formValid" @click="save()" color="primary">Mentés </v-btn>

            </v-card-actions>

        </v-card>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import FormSubTitle from "../basecomponents/FormSubTitle";
    import {servicesService} from "../../services/ServicesService";
    import {toast} from "../../mixins/toast";
    export default {
        name: "Client",
        mixins: [toast],
        components: { FormSubTitle, PageBase },
        data() {
            return {
                formValid: false,
                editable: false,
                newService: false,
                services: [],
                service: {
                    id: null,
                    nev: null,
                    ismetlodo: null,
                    ismetlodesiidoszak: null,
                    rogzitve: null,
                    rogzitette: null,
                }
            }
        },
        mounted() {
            const id = parseInt(this.$route.params.id, 10);
            this.newService = !id;

            if(!this.newService) {
                const service = this.$store.getters.service;
                if(service.id && id === service.id) {
                    this.service = { ...service };
                } else {
                    servicesService.getService(id).then(response => {
                        this.service = response.data;
                        this.$store.commit('setService', service)
                    })
                }
            }
        },
        computed: {
            fieldsEditable() {
                return this.editable || this.newService;
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
                if(this.newService) {
                    this.service.id = 0;
                }
                this.service.rogzitette = this.$store.getters.user.username;
                this.service.rogzitve = new Date().toISOString();

                this.service.egysegar = +this.service.egysegar;
                if(!this.service.ismetlodo) {
                    this.service.ismetlodesiidoszak = 0;
                }
                this.service.ismetlodesiidoszak = +this.service.ismetlodesiidoszak;
                this.service.me = '1';
                this.$store.commit('setService', this.service);
                this.$store.dispatch('saveService').then((response) => {
                    this.saveSuccess();
                     if(this.newService) {
                         this.service.id = response.data;
                     }
                }).catch(() => {
                    this.saveFail();
                })
            }
        }
    }
</script>