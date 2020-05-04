<template>

    <PageBase title="Munkalapok kezelése" width="12" :functions="[
        {
            cb: exportWorksheets,
            icon: 'mdi-file-download-outline',
            tooltip: 'Szolgáltatás adatok exportálása'
        }
    ]">

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
    import {exportToCsv} from "../../utils/ExportToCsv";

    export default {
        name: "Worksheets",
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
            async exportWorksheets() {
                let listToExport = [];
                const win = window.open('about:blank');

                for (const worksheet of this.worksheets) {
                    let wsitem = {
                      ...worksheet.ugyfel,
                      ...worksheet
                    };
                  delete wsitem.ugyfel;

                  let response = await worksheetService.getWorksheetDetails(wsitem.id);
                  worksheet.tetelek = response.data;

                  worksheet.tetelek.forEach((tetel, index) => {
                      Object.keys(tetel).forEach(key => {
                          if(key === 'szolgaltatas') {
                              Object.keys(tetel.szolgaltatas).forEach(szolgKey => {
                                  wsitem[index + '_tetel_' + key + '_' + szolgKey] = tetel.szolgaltatas[szolgKey];
                              })
                          } else {
                              wsitem[index + '_tetel_' + key] = tetel[key];
                          }
                      });
                  });
                  delete wsitem.tetelek;
                  listToExport.push(wsitem);
                }
                win.location = encodeURI(exportToCsv(listToExport));
            },
            editWorksheet(worksheet) {
                this.$store.commit('setWorksheet', worksheet);
                this.$router.push('worksheet/' + worksheet.id);
            },
            deleteWorksheet(worksheet) {
                worksheetService.removeWorksheet(worksheet.id).then(() => {
                    worksheetService.getWorksheetList().then(response => {
                        this.worksheets = response.data;
                        this.filterdWorksheets = response.data;
                        this.filterWorksheets();
                    });
                })
            },
            filterWorksheets() {
                if(this.$route.query.open) {
                    this.filterdWorksheets = this.worksheets.filter(worksheet => {
                        return !worksheet.lezarva;
                    })
                }
                if(this.$route.query.closed) {
                    this.filterdWorksheets = this.worksheets.filter(worksheet => {
                        return !!worksheet.lezarva;
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