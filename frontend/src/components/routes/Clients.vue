<template>

    <PageBase title="Ügyfelek kezelése" width="12" :functions="[
        {
            cb: exportClientData,
            icon: 'mdi-file-download-outline',
            tooltip: 'Ügyfél adatok exportálása'
        },
        {
            cb: () => $router.push('/client'),
            icon: 'mdi-account-plus-outline',
            tooltip: 'Új ügyfél felvitele'
        },
    ]">

        <div slot="content">

            <v-data-table
                    v-model="selected"
                    :headers="headers"
                    :items="clients"
                    item-key="name"
                    class="elevation-5"
            >

                <template v-slot:item.actions="{ item }">

                    <v-icon small class="mr-2" @click="editUser(item)">

                        mdi-pencil

                    </v-icon>

                    <v-icon small @click="openDeleteDialog(item)">

                        mdi-delete

                    </v-icon>

                </template>

            </v-data-table>

            <ConfirmDialog
                    ref="dialog"
                    title="Ügyfél törlése"
                    text="Biztosan törli az ügyfelet?"
                    @confirm="deleteUser()"/>

        </div>

    </PageBase>

</template>

<script>
    import {clientService} from "../../services/ClientService";
    import PageBase from "../basecomponents/PageBase";
    import {exportToCsv} from "../../utils/ExportToCsv";
    import ConfirmDialog from "../basecomponents/ConfirmDialog";
    import {toast} from "../../mixins/toast";

    export default {
        name: "Clients",
        components: {ConfirmDialog, PageBase},
        mixins: [toast],
        methods: {
            exportClientData() {
                let link = document.createElement('a');
                link.href = encodeURI(exportToCsv(this.clients));
                link.download = 'clients.csv';
                link.click();
            },
            editUser(client) {
                this.$store.commit('setClient', client);
                this.$router.push('client/' + client.id);
            },

            deleteUser() {
                return clientService.removeClient(this.userToDelete).then(() => {
                    clientService.getClients().then(response => {
                        this.clients = response.data;
                    });
                    this.userToDelete = null;
                    this.saveSuccess('Ügyfél törlése sikeres!');
                }).catch((error) => {
                    if(error.response.data === 'DATA_IN_USE') {
                        this.saveFail('Az Ügyfél használatban van, nem törölhető!')
                    } else {
                        this.saveFail('Ügyfél törlése sikertelen!');
                    }
                })
            },
            openDeleteDialog(user) {
                this.$refs['dialog'].openDialog();
                this.userToDelete = user;
            },
        },
        mounted() {
            clientService.getClients().then(response => {
                this.clients = response.data;
            })
        },
        data() {
            return {
                userToDelete: null,
                selected: [],
                headers: [
                    {
                        text: 'Név',
                        align: 'start',
                        sortable: false,
                        value: 'nev',
                    },
                    { text: 'Irányítószám', value: 'irszam' },
                    { text: 'Adószám', value: 'adoszam' },
                    { text: 'Város', value: 'telepules' },
                    { text: 'Utca', value: 'kozteruletneve' },
                    { text: 'Lehetőségek', value: 'actions', sortable: false },
                ],
                clients: []
            }
        }
    }
</script>