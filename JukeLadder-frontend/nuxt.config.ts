// https://v3.nuxtjs.org/api/configuration/nuxt.config)
export default defineNuxtConfig({
  css: ['~/assets/css/tailwind.css'],
  build: {
    postcss: {
      postcssOptions: {
        plugins: {
          tailwindcss: {},
          autoprefixer: {}
        }
      }
    }
  },

  imports: {
    dirs: ['composables/**']
  },

  modules: [
    '@nuxtjs/i18n'
  ],

  i18n: {
    locales: [
      {
        code: 'en',
        file: 'en-US.json',
        iso: 'en-US',
        name: 'English',
        flag: 'gb'
      },
      {
        code: 'fr',
        file: 'fr-FR.json',
        iso: 'fr-FR',
        name: 'Français',
        flag: 'fr'
      }
    ],
    strategy: 'prefix_except_default',
    lazy: true,
    langDir: 'lang',
    defaultLocale: 'en',
    baseUrl: process.env.BASE_URL
  },

  publicRuntimeConfig: {
    API_BASE_URL: process.env.API_BASE_URL,
    KEYCLOACK_URL: process.env.KEYCLOACK_URL,
    KEYCLOACK_CLIENT_ID: process.env.KEYCLOACK_CLIENT_ID,
    KEYCLOACK_CLIENT_SECRET: process.env.KEYCLOACK_CLIENT_SECRET,
    KEYCLOACK_ADMIN_ROLE: process.env.KEYCLOACK_ADMIN_ROLE,
    KEYCLOACK_MANAGER_ROLE: process.env.KEYCLOACK_MANAGER_ROLE,
    PRODUCTION: process.env.PRODUCTION === 'true',
    STRIPE_SECRET_KEY: process.env.STRIPE_SECRET_KEY,
    PROMOTE_PRODUCT_ID: process.env.PROMOTE_PRODUCT_ID,
    DEMOTE_PRODUCT_ID: process.env.DEMOTE_PRODUCT_ID,
    DEEZER_APP_ID: process.env.DEEZER_APP_ID,
    DEEZER_CHANNEL_URL: process.env.DEEZER_CHANNEL_URL
  }
})
