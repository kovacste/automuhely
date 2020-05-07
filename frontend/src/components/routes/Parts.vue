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

                    <v-icon small @click="openPartDeleteDialog(item)">

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

                        <v-text-field disabled label="Beszerzési ár" v-model="partToPrice.beszerar" />

                        <span> Aktuális eladási ár {{ initialEladasiar }} Ft</span>

                        <v-text-field disabled label="Új eladási ár" v-model="partToPrice.eladasiar" />

                        <v-text-field
                                suffix="%"
                                label="Haszonkulcs"
                                v-mask="'###'"
                                v-model="percent"
                                @input="changePrice()"
                        />

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

            <ConfirmDialog
                    ref="dialog"
                    title="Alkatrész törlése"
                    text="Biztosan törli az alkatrészt?"
                    @confirm="deletePart()"/>

        </div>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { partService } from "../../services/PartService";
    import {exportToCsv} from "../../utils/ExportToCsv";
    import {MANAGER} from "../../User";
    import {toast} from "../../mixins/toast";
    import ConfirmDialog from "../basecomponents/ConfirmDialog";

    export default {
        name: "Clients",
        components: {ConfirmDialog, PageBase },
        mixins: [toast],
        computed: {
            isManager() {
                return this.$store.getters.user.modules.includes(MANAGER);
            }
        },
        methods: {
            changePrice() {
               this.partToPrice.eladasiar = Math.round(this.initialBeszerar * ((+this.percent + 100) / 100));
            },
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
                this.initialBeszerar = this.partToPrice.beszerar;
                this.initialEladasiar = this.partToPrice.eladasiar;
                this.percent = ((this.initialEladasiar / this.initialBeszerar) * 100) - 100;
                this.partPricingDialog = true;
            },
            savePartPrice() {
                partService.setPartPrice(this.partToPrice).then(() => {
                    this.saveSuccess();
                }).catch(() => {
                    this.saveFail();
                })
            },
            deletePart() {
                partService.removePart(this.partToDelete).then(() => {
                    partService.getPartList().then(response => {
                        this.parts = response.data;
                    });
                    this.partToDelete = null;
                    this.saveSuccess('Az alkatrész törlése sikeres!');
                }).catch(error => {
                    if(error.response.data === 'DATA_IN_USE') {
                        this.saveFail('Az alkatrész használatban van, nem törölhető!');
                    } else {
                        this.saveFail('Az alkatrész törlése sikertelen!');
                    }
                })
            },
            openPartDeleteDialog(part) {
                this.$refs['dialog'].openDialog();
                this.partToDelete = part;
            }
        },
        mounted() {
            partService.getPartList().then(response => {
                this.parts = response.data;
            });
        },
        data() {
            return {
                partToDelete: null,
                initialEladasiar: 0,
                initialBeszerar: 0,
                percent: 100,
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