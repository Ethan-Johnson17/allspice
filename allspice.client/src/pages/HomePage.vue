<template>
  <div class="container">
    <div class="row">
      <h1 class="col-md-12 bg-dark text-white p-3 rounded text-center">
        AllSpice
      </h1>
      <div class="col-md-4" v-for="recipe in recipes" :key="recipe.id">
        <Recipe :recipe="recipe" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import { recipesService } from "../services/RecipesService"
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAll('api/Recipes')
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      recipes: computed(() => AppState.recipes),
    }
  }
}
</script>

<style scoped lang="scss">
</style>
