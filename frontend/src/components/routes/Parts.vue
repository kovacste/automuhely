<template>

    <PageBase title="Alkatrészek kezelése" width="12" :functions="[
        { cb: exportPartData, icon: 'mdi-file-download-outline', tooltip: 'Alkatrész adatok exportálása' },
    ]">

        <div slot="content">

            <v-data-table
                    v-model="selected"
                    :headers="headers"
                    :items="parts"
                    item-key="name"
                    class="elevation-5"
            >

                <template v-slot:item.actions="{ item }">

                    <v-icon small class="mr-2" @click="editPart(item)">

                        mdi-pencil

                    </v-icon>

                    <v-icon v-if="isManager" small class="mr-2" @click="setPrice(item)">

                        mdi-percent

                    </v-icon>

                    <v-icon small @click="deletePart(item)">

                        mdi-delete

                    </v-icon>


                </template>

            </v-data-table>

            <v-dialog v-model="partPricingDialog" width="700">

                <v-card>

                    <v-card-title class="headline grey lighten-2" primary-title>

                        Alkatrész árazása

                    </v-card-title>

                    <v-card-text>

                        <v-text-field label="Beszerzési ár" v-model="partToPrice.beszerar" />

                        <v-text-field label="Eladási ár" v-model="partToPrice.eladasiar" />

                        <v-btn @click="savePartPrice()"> Mentés </v-btn>

                    </v-card-text>

                    <v-divider />

                    <v-card-actions>

                        <v-spacer />

                        <v-btn
                                color="primary"
                                text
                                @click="partPricingDialog = false"
                        >

                            Mégse

                        </v-btn>

                    </v-card-actions>

                </v-card>

            </v-dialog>

        </div>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { partService } from "../../services/PartService";
    import {exportToCsv} from "../../utils/ExportToCsv";
    import {MANAGER} from "../../User";
    import {toast} from "../../mixins/toast";

    export default {
        name: "Clients",
        components: { PageBase },
        mixins: [toast],
        computed: {
            isManager() {
                return this.$store.getters.user.modules.includes(MANAGER);
            }
        },
        methods: {
            exportPartData() {
                let link = document.createElement('a');
                link.href = encodeURI(exportToCsv(this.parts));
                link.download = 'parts.csv';
                link.click();
            },
            editPart(part) {
                this.$store.commit('setPart', part);
                this.$router.push('part/' + part.id);
            },
            setPrice(part) {
                this.partToPrice = part;
                this.partPricingDialog = true;
            },
            savePartPrice() {
                partService.setPartPrice(this.partToPrice).then(() => {
                    this.saveSuccess();
                }).catch(() => {
                    this.saveFail();
                })
            },
            deletePart(part) {
                partService.removePart(part).then(() => {
                    partService.getPartList().then(response => {
                        this.parts = response.data;
                    });
                });
            }
        },
        mounted() {
            partService.getPartList().then(response => {
                this.parts = response.data;
            });
        },
        data() {
            return {
                partPricingDialog: false,
                partToPrice: {},
                selected: [],
                headers: [
                    {
                        text: 'Név',
                        align: 'start',
                        sortable: false,
                        value: 'nev',
                    },
                    { text: 'Beszerzési ár', value: 'beszerar' },
                    { text: 'Eladási ár', value: 'eladasiar' },
                    { text: 'Lehetőségek', value: 'actions', sortable: false },
                ],
                parts: []
            }
        }
    }
</script>