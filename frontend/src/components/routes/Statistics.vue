<template>
    <PageBase title="Statisztikák" width="12">
        <GChart slot="content"
                type="ColumnChart"
                :data="chartData"
                :options="chartOptions"
        />
    </PageBase>
</template>

<script>
    import PageBase from "../basecomponents/PageBase";
    import { GChart } from 'vue-google-charts';
    export default {
        name: "Statistics",
        components: {PageBase, GChart},
        data() {
            return {
                chartData: [
                    ['Year', 'Sales', 'Expenses', 'Profit'],
                    ['2014', 1000, 400, 200],
                    ['2015', 1170, 460, 250],
                    ['2016', 660, 1120, 300],
                    ['2017', 1030, 540, 350]
                ],
                chartOptions: {
                    chart: {
                        title: 'Company Performance',
                        subtitle: 'Sales, Expenses, and Profit: 2014-2017',
                    }
                }
            }
        }
    }
</script>

<style scoped>

</style>