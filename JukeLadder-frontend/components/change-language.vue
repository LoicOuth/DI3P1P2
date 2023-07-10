<template>
  <div class="font-normal text-black">
    <img
      class="w-10 h-10 p-1 mr-5 rounded-full cursor-pointer hover:bg-secondary/20"
      src="~~/assets/icons/globe-solid.svg"
      alt="change language"
      @click="(showPopup = true)"
    >

    <FormsModalComponent v-if="showPopup" :title="$t('popupLanguage.title')" @close="(showPopup = false)">
      <div class="flex flex-wrap">
        <div
          v-for="loc in (locales as Array<LocaleObject>)"
          :key="loc.code"
          class="relative flex items-center justify-center w-full px-10 py-5 my-3 rounded-md cursor-pointer lg:w-auto lg:ml-5 lg:my-10 hover:bg-black/20"
          :class="{'bg-primary/10' : locale === loc.code}"
          @click="switchLang(loc.code)"
        >
          <CountryFlag :country="loc.flag" class="mr-5 rounded-md" size="big" />
          <h5 class="ml-5">
            {{
              loc.name
            }}
          </h5>
        </div>
      </div>
    </FormsModalComponent>
  </div>
</template>

<script setup lang="ts">
import { LocaleObject } from '@nuxtjs/i18n/dist/runtime/composables'

const showPopup = ref<boolean>(false)

const { locales, locale } = useI18n()
const switchLocalePath = useSwitchLocalePath()

const switchLang = (to: string) => {
  navigateTo(switchLocalePath(to))
  showPopup.value = false
}
</script>
