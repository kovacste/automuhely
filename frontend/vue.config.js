module.exports = {
  "runtimeCompiler": true,
  "transpileDependencies": [
    "vuetify"
  ],
  devServer: {
    proxy: {
      '/api': {
        target:  'http://52.170.117.6'
      }
    }
  }
};