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

        <v-data-table
                slot="content"
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

                <v-icon small @click="deleteUser(item)">

                    mdi-delete

                </v-icon>

            </template>

        </v-data-table>

    </PageBase>

</template>

<script>
    import {clientService} from "../../services/ClientService";
    import PageBase from "../basecomponents/PageBase";
    import {exportToCsv} from "../../utils/ExportToCsv";

    export default {
        name: "Clients",
        components: {PageBase},
        methods: {
            exportClientData() {
                window.open(encodeURI(exportToCsv(this.clients)));
            },
            editUser(client) {
                this.$store.commit('setClient', client);
                this.$router.push('client/' + client.id);
            },

            deleteUser(user) {
                return clientService.removeClient(user).then(() => {
                    clientService.getClients().then(response => {
                        this.clients = response.data;
                    });
                });
            }
        },
        mounted() {
            clientService.getClients().then(response => {
                this.clients = response.data;
            })
        },
        data() {
            return {
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
                    { text: 'Actions', value: 'actions', sortable: false },
                ],
                clients: []
            }
        }
    }
</script>