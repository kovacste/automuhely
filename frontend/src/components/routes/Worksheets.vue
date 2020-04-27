<template>

    <PageBase title="Munkalapok kezelése" width="12">

        <v-data-table
                slot="content"
                v-model="selected"
                :headers="headers"
                :items="worksheets"
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
        methods: {
            editWorksheet(worksheet) {
                this.$store.commit('setWorksheet', worksheet);
                this.$router.push('worksheet/' + worksheet.id);
            },
            deleteWorksheet(worksheet) {
                console.log(worksheet);
            }
        },
        mounted() {
            worksheetService.getWorksheetList().then(response => {
                this.worksheets = response.data;
            })
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
                worksheets: []
            }
        }
    }
</script>