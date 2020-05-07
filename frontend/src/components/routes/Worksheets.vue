<template>

    <PageBase title="Munkalapok kezelése" width="12" :functions="[
        {
            cb: exportWorksheets,
            icon: 'mdi-file-download-outline',
            tooltip: 'Szolgáltatás adatok exportálása'
        }
    ]">

        <div slot="content">

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

                    <v-icon small @click="openDeleteDialog(item)">

                        mdi-delete

                    </v-icon>

                </template>

            </v-data-table>

            <ConfirmDialog
                    ref="dialog"
                    title="Munkalap törlése"
                    text="Biztosan törli a munkalapot?"
                    @confirm="deleteWorksheet()"/>

        </div>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { worksheetService } from "../../services/WorksheetService";
    import {exportToCsv} from "../../utils/ExportToCsv";
    import ConfirmDialog from "../basecomponents/ConfirmDialog";
    import {toast} from "../../mixins/toast";
    import {CLIENT} from "../../User";

    export default {
        name: "Worksheets",
        components: {ConfirmDialog, PageBase },
        mixins: [toast],
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
                let link = document.createElement('a');
                link.href = encodeURI(exportToCsv(listToExport));
                link.download = 'worksheets.csv';
                link.click();
            },
            editWorksheet(worksheet) {
                this.$store.commit('setWorksheet', worksheet);
                this.$router.push('worksheet/' + worksheet.id);
            },
            deleteWorksheet() {
                if(this.worksheetToDelete.lezarva) {
                    this.saveFail('Lezárt munkalap nem törölhető!');
                } else {
                    worksheetService.removeWorksheet(this.worksheetToDelete.id).then(() => {
                        worksheetService.getWorksheetList().then(response => {
                            this.worksheets = response.data;
                            this.filterdWorksheets = response.data;
                            this.filterWorksheets();
                        });
                        this.saveSuccess('Munkalap törlése sikeres!');
                    }).catch(() => {
                        this.saveFail('Munkalap törlése sikertelen!')
                    })
                }
            },
            openDeleteDialog(worksheet) {
                this.$refs['dialog'].openDialog();
                this.worksheetToDelete = worksheet;
            },
            filterWorksheets() {
                if(this.$route.query.open) {
                    this.filterdWorksheets = this.worksheets.filter(worksheet => {
                        return !worksheet.lezarva;
                    });
                }
                if(this.$route.query.closed) {
                    this.filterdWorksheets = this.worksheets.filter(worksheet => {
                        return !!worksheet.lezarva;
                    });
                }

                this.filterdWorksheets.sort((a, b) => {
                    return b.id - a.id;
                });
            }
        },
        mounted() {

            console.log(this.$store.getters.user.modules)
            const listService = this.$store.getters.user.modules.includes(CLIENT)
                ? () => worksheetService.getWorkSheetWithClientId(this.$store.getters.user.id)
                : () => worksheetService.getWorksheetList();

            listService().then(response => {
                this.worksheets = response.data;
                this.filterdWorksheets = response.data;
                this.filterWorksheets();
            });

           /* if(this.$store.getters.user.modules.include(CLIENT)) {
                worksheetService.getWorkSheetWithClientId(this.$store.getters.user.id).then(response => {
                    this.worksheets = response.data;
                    this.filterdWorksheets = response.data;
                    this.filterWorksheets();
                });
            } else {
                worksheetService.getWorksheetList().then(response => {
                    this.worksheets = response.data;
                    this.filterdWorksheets = response.data;
                    this.filterWorksheets();
                });
            }*/
        },
        data() {
            return {
                worksheetToDelete: null,
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
                    { text: 'Lehetőségek', value: 'actions', sortable: false },
                ],
                worksheets: [],
                filterdWorksheets: []
            }
        }
    }
</script>