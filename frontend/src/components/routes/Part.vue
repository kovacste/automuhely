<template>
    <PageBase title="Alkatrész adatok" width="8">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Alkatrész adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                <v-form>

                    <FormSubTitle :active="fieldsEditable" title="Alapadatok" />

                    <v-layout row class="pa-1 ma-2">

                        <v-flex md1 xs1 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="part.id"
                                    label="Azonosító"
                                    name="id"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="part.nev"
                                    label="Alkatrész neve"
                                    name="nev"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="part.beszerar"
                                    label="Beszerzési ár"
                                    name="beszerar"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md3 xs12 class="ma-2">

                            <v-text-field
                                    :disabled="!fieldsEditable"
                                    v-model="part.eladasiar"
                                    label="Eladási ár"
                                    name="eladasiar"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />
                        </v-flex>


                    </v-layout>

                    <FormSubTitle :active="false" title="Egyéb" />

                    <v-layout row class="ma-2 pa-1">

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="part.rogzitve"
                                    label="Rögzítve"
                                    name="rogzitve"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                        <v-flex md4 xs12 class="ma-2">

                            <v-text-field
                                    disabled
                                    v-model="part.rogzitette"
                                    label="Rögzítette"
                                    name="rogzitette"
                                    type="text"
                                    :rules="[v => !!v || 'Kötelező mező!']"
                            />

                        </v-flex>

                    </v-layout>

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
    import FormSubTitle from "../basecomponents/FormSubTitle";
    export default {
        name: "Client",
        components: { FormSubTitle, PageBase },
        data() {
            return {
                editable: false,
                newPart: false,
                parts: [],
                part: {
                    id: null,
                    nev: null,
                    beszerar: null,
                    eladasiar: null,
                    rogzitve: null,
                    rogzitette: null
                }
            }
        },
        mounted() {
            const id = parseInt(this.$route.params.id, 10);
            const part = this.$store.getters.part;
            this.newPart = !id;
            if(id && id === part.id) {
                this.part = { ...part };
            }
        },
        computed: {
            fieldsEditable() {
                return this.editable || this.newPart;
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
                if(this.newPart) {
                    this.part.id = 0;
                }
                this.part.rogzitette = this.$store.getters.user.username;
                this.$store.commit('setPart', this.part);
                this.$store.dispatch('savePart').then(() => {
                     this.$toasted.show('Alkatrész mentése sikeres!',{
                        theme: "toasted-primary", 
                        position: "bottom-right", 
                        duration : 5000
                    });
                });
            }
        }
    }
</script>