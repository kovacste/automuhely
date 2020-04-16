module.exports = {
  "runtimeCompiler": true,
  "transpileDependencies": [
    "vuetify"
  ],
  devServer: {
    proxy: {
      '/api': {
        target:  'http://13.82.60.160'
      }
    }
  }
};