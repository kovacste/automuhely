<template>
    <PageBase title="Ügyfél adatok" width="12">

        <v-card slot="content" class="elevation-12">

            <v-toolbar color="primary" dark flat>

                <v-toolbar-title> Ügyfél adatai  </v-toolbar-title>

            </v-toolbar>

            <v-card-text>

                <v-form>

                    <v-text-field
                            label="Felhasználónév"
                            name="login"
                            type="text"
                    />

                    <v-text-field
                            id="Jelszó"
                            label="Password"
                            name="password"
                            type="password"
                    />
                </v-form>

            </v-card-text>

            <v-card-actions>

                <v-spacer />

                <v-btn @click="login()" color="primary">Mentés </v-btn>

            </v-card-actions>

        </v-card>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    export default {
        name: "Client",
        components: {PageBase}
    }
</script>