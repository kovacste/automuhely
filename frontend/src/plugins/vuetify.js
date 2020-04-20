import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import colors from 'vuetify/lib/util/colors'

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        themes: {
            light: {
                primary: colors.grey.darken1,
                secondary: colors.brown.lighten5,
                accent: colors.indigo.base,
            },
            dark: {
                primary: colors.brown.darken4,
                secondary: colors.grey.lighten5,
                accent: colors.indigo.base
            }
        },
    },
});
