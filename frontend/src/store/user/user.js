export const user = {
    state: {
        user: null
    },

    getters: {
        user: state => state.user
    },

    mutations: {
        setUser: (state, user) => state.user = user,
        logOutUser: (state) => state.user = null
    },

    actions: {

    }
};