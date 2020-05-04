export const toast = {
    methods: {
        saveFail(message = 'Mentés sikertelen!') {
            this.$toasted.show(message,{
                theme: "toasted-primary",
                position: "bottom-right",
                duration : 5000
            });
        },
        saveSuccess(message = 'Mentés sikeres!') {
            this.$toasted.show(message,{
                theme: "toasted-primary",
                position: "bottom-right",
                duration : 5000
            });
        }
    }
};