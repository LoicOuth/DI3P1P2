module.exports = {
  content: [
    './components/**/*.{js,vue,ts}',
    './layouts/**/*.vue',
    './pages/**/*.vue',
    './plugins/**/*.{js,ts}',
    './nuxt.config.{js,ts}',
    'app.vue',
    './assets/icons/*.svg'
  ],
  theme: {
    extend: {
      colors: {
        primary: '#14213D',
        secondary: '#FCA311',
        third: '#E5E5E5',
        success: '#22c55e',
        error: '#8E2C2C'
      }
    }
  },
  plugins: []
}
