<template>
  <div class="container">
    <div class="row justify-content-center">
      <h1 class="col-md-6 text-dark p-3 rounded text-center">AllSpice</h1>
    </div>
    <div class="row justify-content-evenly">
      <div class="col-md-3" v-for="recipe in recipes" :key="recipe.id">
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
