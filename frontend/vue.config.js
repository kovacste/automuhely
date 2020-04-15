module.exports = {
  "runtimeCompiler": true,
  "transpileDependencies": [
    "vuetify"
  ],
  devServer: {
    proxy: {
      '/': {
        target:  'http://localhost:5000'
      }
    }
  }
};