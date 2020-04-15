import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import colors from 'vuetify/lib/util/colors'

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        themes: {
            light: {
                primary: colors.brown.darken4,
                secondary: colors.brown.lighten1,
                accent: colors.indigo.base,
            },
        },
    },
});
