<template>
    <div>
        <h3 class="title form-title" :class="getClass"> {{ title }} </h3>
        <hr class="form-title" :class="getClass"/>
    </div>
</template>

<script>
    export default {
        name: "FormSubTitle",
        props: {
            title: {
                required: true,
                type: String
            },
            active: {
                type: Boolean,
                default: true
            }
         },
        computed: {
            getClass() {
               return {
                   'inactive-class' : !this.active
               }
            }
        }
    }
</script>

<style scoped>
    .form-title {
        color: rgba(0, 0, 0, 0.87);
    }
    .inactive-class {
        color: rgba(0, 0, 0, 0.5)
    }
</style>