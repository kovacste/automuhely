<template>
    <PageBase title="Statisztikák" width="12">

        <template slot="content">

            <FormSubTitle title="Felvitt munkalapok száma dolgozonként"/>

            <v-layout style="margin-left: 10%;" class="mt-5">

                <v-flex md1 class="pa-1">

                    <v-select
                            v-model="workerStatEv"
                            label="Év"
                            :items="[2020, 2021]"
                            @change="recalcWorkerStat()"
                    />

                </v-flex>

                <v-flex md1 class="pa-1">

                    <v-select
                            v-model="workerStatHo"
                            label="Hónap"
                            :items="[1,2,3,4,5,6,7,8,9,10,11,12]"
                            @change="recalcWorkerStat()"
                    />

                </v-flex>

            </v-layout>

            <GChart type="ColumnChart"
                    :data="workerStat"
                    :options="workerStatOpts"
            />

            <v-divider class="ma-5"/>

            <FormSubTitle title="Lezárt munkalapok száma dolgozonként"/>

            <v-layout style="margin-left: 10%;" class="mt-5">

                <v-flex md1 class="pa-1">

                    <v-select
                            v-model="serviceStatEv"
                            label="Év"
                            :items="[2020, 2021]"
                            @change="recalcServiceStat()"
                    />

                </v-flex>

                <v-flex md1 class="pa-1">

                    <v-select
                            v-model="serviceStatHo"
                            label="Hónap"
                            :items="[1,2,3,4,5,6,7,8,9,10,11,12]"
                            @change="recalcServiceStat()"
                    />

                </v-flex>

            </v-layout>

            <GChart type="ColumnChart"
                    :data="serviceStat"
                    :options="serviceStatOpts"
            />

        </template>

    </PageBase>

</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { GChart } from 'vue-google-charts';
    import {reportService} from "../../services/ReportService";
    import FormSubTitle from "../basecomponents/FormSubTitle";
    export default {
        name: "Statistics",
        components: {FormSubTitle, PageBase, GChart},
        methods: {
            recalcWorkerStat() {
                this.workerStat =  [
                    ['Név', 'Felvitt munkalapok'],
                ];
                reportService.getWorkerStatistics(this.workerStatEv, this.workerStatHo).then(response => {
                    const statArray = response.data;
                    statArray.forEach(stat => {
                        this.workerStat.push([
                            stat.nev,
                            stat.munkalapDb
                        ]);
                    })
                });
            },

            recalcServiceStat() {
                this.serviceStat = [
                    ['Név', 'Lezárt munkalapok'],
                ];
                reportService.getServiceStatistics(this.serviceStatEv, this.serviceStatHo).then(response => {
                    const statArray = response.data;
                    statArray.forEach(stat => {
                        this.serviceStat.push([
                            stat.nev,
                            stat.munkalapDb
                        ]);
                    })
                });
            }
        },
        mounted() {
          this.recalcWorkerStat();
          this.recalcServiceStat();
        },
        data() {
            return {
                workerStatEv: 2020,
                workerStatHo: 5,
                serviceStatEv: 2020,
                serviceStatHo: 5,
                workerStat: [],
                workerStatOpts: {
                    chart: {
                        title: 'Company Performance',
                        subtitle: '',
                    }
                },
                serviceStat: [],
                serviceStatOpts: {
                    chart: {
                        title: 'Company Performance',
                        subtitle: 'Sales, Expenses, and Profit: 2014-2017',
                    }
                },
            }
        }
    }
</script>

<style scoped>

</style>