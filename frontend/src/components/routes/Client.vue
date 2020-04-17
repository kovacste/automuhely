<template>
    <PageBase title="Ügyfél adatok" width="12">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Ügyfél adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                {{ newClient }}
                <v-form>

                    <v-text-field
                            disabled
                            v-model="client.id"
                            label="Azonosító"
                            name="id"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.nev"
                            label="Név"
                            name="nev"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!adoszamEditable"
                            v-model="client.adoszam"
                            label="Adószám"
                            name="adoszam"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.irszam"
                            label="Irányítószám"
                            name="irszam"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.telepulesid"
                            label="Település azonosító"
                            name="telepulesid"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.telepules"
                            label="Település"
                            name="telepules"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.kozteruletneve"
                            label="Közterület neve"
                            name="kozteruletneve"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.kozteruletjellegid"
                            label="Közterület jelleg azonosító"
                            name="kozteruletjellegid"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.kozteruletjelleg"
                            label="Közterület jellege"
                            name="kozteruletjelleg"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.hazszam"
                            label="Házszám"
                            name="hazszam"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.telefonszam"
                            label="Telefonszám"
                            name="telefonszam"
                            type="text"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            :disabled="!fieldsEditable"
                            v-model="client.email"
                            label="E-mail cím"
                            name="email"
                            type="text"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            disabled
                            v-model="client.rogzitve"
                            label="Rögzítve"
                            name="rogzitve"
                            type="text"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                    <v-text-field
                            disabled
                            v-model="client.rogzitette"
                            label="Rögzítette"
                            name="rogzitette"
                            type="text"
                            :rules="[v => !!v || 'Kötelező mező!']"
                    />

                </v-form>

            </v-card-text>

            <v-card-actions>

                <v-spacer />

                <v-btn v-if="!fieldsEditable" @click="edit()" color="primary">Módosítás </v-btn>

                <v-btn v-if="fieldsEditable" @click="cancel()" color="primary">Mégse </v-btn>

                <v-btn v-if="fieldsEditable" @click="save()" color="primary">Mentés </v-btn>

            </v-card-actions>

        </v-card>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    export default {
        name: "Client",
        components: { PageBase },
        data() {
            return {
                editable: false,
                newClient: false,
                client: {
                   id: null,
                   nev: null,
                   adoszam: null,
                   irszam: null,
                   telepulesid: null,
                   telepules: null,
                   kozteruletneve: null,
                   kozteruletjellegid: null,
                   kozteruletjelleg: null,
                   hazszam: null,
                   telefonszam: null,
                   email: null,
                   rogzitve: null,
                   rogzitette: null,
                }
            }
        },
        mounted() {
            const id = parseInt(this.$route.params.id, 10);
            const client = this.$store.getters.client;
            this.newClient = !id;
            if(id && id === client.id) {
                this.client = { ...client }
            }
        },
        computed: {
            adoszamEditable() {
                return !this.client.id && this.fieldsEditable
            },
            fieldsEditable() {
                return this.editable || this.newClient
            }
        },
        methods: {
            edit() {
                this.editable = true;
            },
            cancel() {
                this.editable = false;
            },
            save() {
                this.editable = false;
                this.$store.commit('setClient', this.client);
                this.$store.dispatch('saveClient').then(response => {
                    console.log(response)
                })
            },
        }
    }
</script>