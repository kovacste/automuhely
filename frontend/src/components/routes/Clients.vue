<template>

    <PageBase title="Ügyfelek kezelése" width="12">

        <v-data-table
                slot="content"
                v-model="selected"
                :headers="headers"
                :items="clients"
                item-key="name"
                show-select
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

    export default {
        name: "Clients",
        components: {PageBase},
        methods: {
            editUser(client) {
                this.$store.commit('setClient', client);
                this.$router.push('client/' + client.id);
            },

            deleteUser(user) {
                console.log(user, 'user to delete');
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