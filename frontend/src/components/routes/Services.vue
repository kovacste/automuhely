<template>

    <PageBase title="Szolgáltatások kezelése" width="12" :functions="[
        {
            cb: exportServiceData,
            icon: 'mdi-file-download-outline',
            tooltip: 'Szolgáltatás adatok exportálása'
        },
        {
            cb: () => $router.push('/service'),
            icon: 'mdi-account-plus-outline',
            tooltip: 'Új szolgáltatás felvitele'
        },
    ]">

        <div slot="content">

            <v-data-table
                    slot="content"
                    v-model="selected"
                    :headers="headers"
                    :items="services"
                    item-key="name"
                    class="elevation-5"
            >

                <template v-slot:item.actions="{ item }">

                    <v-icon small class="mr-2" @click="editService(item)">

                        mdi-pencil

                    </v-icon>

                    <v-icon small @click="openDeleteDialog(item)">

                        mdi-delete

                    </v-icon>

                </template>

            </v-data-table>

            <ConfirmDialog
                    ref="dialog"
                    title="Szolgáltatás törlése"
                    text="Biztosan törli a szolgáltatást?"
                    @confirm="deleteService()"/>

        </div>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { servicesService } from "../../services/ServicesService";
    import { exportToCsv } from "../../utils/ExportToCsv";
    import ConfirmDialog from "../basecomponents/ConfirmDialog";
    import {toast} from "../../mixins/toast";

    export default {
        name: "Services",
        components: {ConfirmDialog, PageBase },
        mixins: [toast],
        methods: {
            exportServiceData() {
                let link = document.createElement('a');
                link.href = encodeURI(exportToCsv(this.services));
                link.download = 'services.csv';
                link.click();
            },
            editService(service) {
                this.$store.commit('setService', service);
                this.$router.push('service/' + service.id);
            },
            deleteService() {
                this.serviceToDelete.rogzitette = this.$store.getters.user.username;
                servicesService.removeService(this.serviceToDelete).then(() => {
                    servicesService.getServiceList().then(response => {
                        this.services = response.data;
                    });
                    this.serviceToDelete = null;
                    this.saveSuccess('Szolgáltatás törlése sikeres!');
                }).catch(error => {
                    if(error.response.data === 'DATA_IN_USE') {
                        this.saveFail('A szolgáltatás használatban van, nem törölhető!');
                    } else {
                        this.saveFail('Szolgáltatás törlése sikertelen!');
                    }
                })
            },
            openDeleteDialog(service) {
                this.$refs['dialog'].openDialog();
                this.serviceToDelete = service;
            }
        },
        mounted() {
            servicesService.getServiceList().then(response => {
                this.services = response.data;
            });
        },
        data() {
            return {
                serviceToDelete: null,
                selected: [],
                headers: [
                    {
                        text: 'Név',
                        align: 'start',
                        sortable: false,
                        value: 'nev',
                    },
                    { text: 'Irányítószám', value: 'irszam' },
                    { text: 'Darab', value: 'me' },
                    { text: 'Egységár', value: 'egysegar' },
                    { text: 'Ismétlődő', value: 'ismetlodo' },
                    { text: 'Lehetőségek', value: 'actions', sortable: false },
                ],
                services: []
            }
        }
    }
</script>