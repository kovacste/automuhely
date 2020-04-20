<template>

    <PageBase title="Alkatrészek kezelése" width="12">

        <v-data-table
                slot="content"
                v-model="selected"
                :headers="headers"
                :items="parts"
                item-key="name"
                show-select
                class="elevation-5"
        >

            <template v-slot:item.actions="{ item }">

                <v-icon small class="mr-2" @click="editPart(item)">

                    mdi-pencil

                </v-icon>

                <v-icon small @click="deletePart(item)">

                    mdi-delete

                </v-icon>

            </template>

        </v-data-table>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { partService } from "../../services/PartService";

    export default {
        name: "Clients",
        components: { PageBase },
        methods: {
            editPart(part) {
                this.$store.commit('setPart', part);
                this.$router.push('part/' + part.id);
            },

            deletePart(part) {
                console.log(part);
            }
        },
        mounted() {
            partService.getPartList().then(response => {
                this.parts = response.data;
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
                    { text: 'Beszerzési ár', value: 'beszerar' },
                    { text: 'Eladási ár', value: 'eladasiar' },
                    { text: 'Actions', value: 'actions', sortable: false },
                ],
                parts: []
            }
        }
    }
</script>