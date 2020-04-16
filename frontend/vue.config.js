module.exports = {
  "runtimeCompiler": true,
  "transpileDependencies": [
    "vuetify"
  ],
  devServer: {
    proxy: {
      '/api': {
        target:  '13.82.60.160'
      }
    }
  }
};