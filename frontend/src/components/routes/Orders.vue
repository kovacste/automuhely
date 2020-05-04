<template>
    <PageBase title="Alkatrész rendelések" width="12" :functions="[

    ]">

        <v-data-table
                slot="content"
                v-model="selected"
                :headers="headers"
                :items="orders"
                item-key="name"
                class="elevation-5"
        >

            <template v-slot:item.actions="{ item }">

                <v-icon small class="mr-2" @click="deleteOrder(item)">

                    mdi-pencil

                </v-icon>

                <v-icon small @click="editWorksheet(item)">

                    mdi-delete

                </v-icon>

            </template>

        </v-data-table>

    </PageBase>
</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { worksheetService } from "../../services/WorksheetService";
    import { exportToCsv } from "../../utils/ExportToCsv";
    export default {
        name: "Orders",
        components: { PageBase },
        methods: {
            exportOrderData() {
                window.open(encodeURI(exportToCsv(this.orders)));
            },
            editWorksheet(client) {
                this.$router.push('client/' + client.id);
            },
            deleteOrder(user) {
                console.log(user, 'user to delete');
            }
        },
        mounted() {
            worksheetService.getWorksheetOrders().then(response => {
                this.orders = response.data;
            })
        },
        data() {
            return {
                selected: [],
                headers: [
                    { text: 'Alkatrész neve', value: 'alkatresz.nev' },
                    { text: 'Mennyiség', value: 'mennyiseg' },
                    { text: 'Rögzítve', value: 'rogzitve' },
                    { text: 'Rögzítette', value: 'rogzitette' },
                    { text: 'Actions', value: 'actions', sortable: false },
                ],
                orders: []
            }
        }
    }
</script>