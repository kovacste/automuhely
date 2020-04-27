<template>

    <PageBase title="Munkalapok kezelése" width="12">

        <v-data-table
                slot="content"
                v-model="selected"
                :headers="headers"
                :items="filterdWorksheets"
                item-key="name"
                class="elevation-5"
        >

            <template v-slot:item.actions="{ item }">

                <v-icon small class="mr-2" @click="editWorksheet(item)">

                    mdi-pencil

                </v-icon>

                <v-icon small @click="deleteWorksheet(item)">

                    mdi-delete

                </v-icon>

            </template>

        </v-data-table>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { worksheetService } from "../../services/WorksheetService";

    export default {
        name: "Clients",
        components: { PageBase },
        watch: {
            queryString() {
                this.filterWorksheets();
            }
        },
        computed: {
            queryString() {
                return this.$route.query;
            }
        },
        methods: {
            editWorksheet(worksheet) {
                this.$store.commit('setWorksheet', worksheet);
                this.$router.push('worksheet/' + worksheet.id);
            },
            deleteWorksheet(worksheet) {
                console.log(worksheet);
            },
            filterWorksheets() {
                if(this.$route.query.open) {
                    this.filterdWorksheets = this.worksheets.filter(worksheet => {
                        return !worksheet.lezarva
                    })
                }
                if(this.$route.query.closed) {
                    this.filterdWorksheets = this.worksheets.filter(worksheet => {
                        return worksheet.lezarva
                    })
                }
            }
        },
        mounted() {
            worksheetService.getWorksheetList().then(response => {
                this.worksheets = response.data;
                this.filterdWorksheets = response.data;
                this.filterWorksheets();
            });
        },
        data() {
            return {
                selected: [],
                headers: [
                    {
                        text: 'Munkalap azonosító',
                        align: 'start',
                        sortable: false,
                        value: 'id',
                    },
                    { text: 'Ügyfél neve', value: 'ugyfel.nev' },
                    { text: 'Időpont', value: 'idopont' },
                    { text: 'Actions', value: 'actions', sortable: false },
                ],
                worksheets: [],
                filterdWorksheets: []
            }
        }
    }
</script>