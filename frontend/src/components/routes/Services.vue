<template>

    <PageBase title="Szolgáltatások kezelése" width="12">

        <v-data-table
                slot="content"
                v-model="selected"
                :headers="headers"
                :items="services"
                item-key="name"
                show-select
                class="elevation-5"
        >

            <template v-slot:item.actions="{ item }">

                <v-icon small class="mr-2" @click="editService(item)">

                    mdi-pencil

                </v-icon>

                <v-icon small @click="deleteService(item)">

                    mdi-delete

                </v-icon>

            </template>

        </v-data-table>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import {servicesService} from "../../services/ServicesService";

    export default {
        name: "Clients",
        components: {PageBase},
        methods: {
            editService(service) {
                this.$store.commit('setService', service);
                this.$router.push('service/' + service.id);
            },

            deleteService(service) {
                console.log(service);
            }
        },
        mounted() {
            servicesService.getServiceList().then(response => {
                this.services = response.data;
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
                    { text: 'Darab', value: 'me' },
                    { text: 'Egységár', value: 'egysegar' },
                    { text: 'Ismétlődő', value: 'ismetlodo' },
                    { text: 'Actions', value: 'actions', sortable: false },
                ],
                services: []
            }
        }
    }
</script>