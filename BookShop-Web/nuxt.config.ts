// https://nuxt.com/docs/api/configuration/nuxt-config
import tailwindcss from "@tailwindcss/vite";
export default defineNuxtConfig({
    compatibilityDate: '2025-05-15',
    devtools: { enabled: false },
    css: ['~/assets/css/main.css'],
    vite: {
        plugins: [
            tailwindcss(),
        ],
    },
    devServer: {
        port: 58603
    },
    modules: [
        '@nuxt/eslint',
        '@nuxt/image',
        '@nuxt/icon',
        '@nuxt/ui'
    ],
    runtimeConfig: {
        public: {
            apiBase: process.env.NUXT_PUBLIC_API_BASE,
            CouponBase: process.env.NUXT_PUBLIC_API_CouponBASE,
            AuthBaseApiLogin: process.env.NUXT_PUBLIC_API_AUTH_LOGIN,
            AuthBaseApiRegister: process.env.NUXT_PUBLIC_API_AUTH_REGISTER,
        }
    },
    icon: {
        serverBundle: {
            collections: ['uil', 'mdi','maki'] 
        }
    }
})
