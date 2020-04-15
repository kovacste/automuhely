<template>

    <v-layout row>

        <Toolbar :title="title"/>

        <v-container class="fill-height" fluid>

            <v-row align="center" justify="center">

                <v-col cols="12" :sm="width" :md="width">

                    <slot name="content" />

                </v-col>

            </v-row>

        </v-container>

    </v-layout>

</template>

<script>

    import Toolbar from "./Toolbar";
    export default {
        name: "PageBase",
        components: {Toolbar},
        props: {
            title: {
                type: String,
                required: true
            },
            width: {
                type: String,
                required: true
            }
        }
    }
</script>

<style scoped>

</style>