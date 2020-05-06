<template>

    <v-dialog v-model="open" width="700">

        <v-card>

            <v-card-title class="headline grey lighten-2" primary-title>

               {{ title }}

            </v-card-title>

            <v-card-text>

                {{ text }}

            </v-card-text>

            <v-divider />

            <v-card-actions>

                <v-spacer />

                <v-btn
                        color="primary"
                        text
                        @click="confirm()"
                >

                    Igen

                </v-btn>

                <v-btn
                        color="primary"
                        text
                        @click="open = false"
                >

                    Mégse

                </v-btn>

            </v-card-actions>

        </v-card>

    </v-dialog>

</template>

<script>
    export default {
        name: "ConfirmDialog",
        props: ['title', 'text'],
        data() {
            return {
                open: false
            }
        },
        methods: {
            openDialog() {
                this.open = true;
            },
            confirm() {
                this.open = false;
                this.$emit('confirm');
            }
        }
    }
</script>