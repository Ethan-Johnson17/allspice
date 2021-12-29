<template>
  <div class="container">
    <div class="row justify-content-center">
      <h1 class="col-md-6 text-dark p-3 rounded text-center">AllSpice</h1>
    </div>
    <div class="row justify-content-evenly">
      <div
        class="col-md-3 mx-3 my-4 px-0"
        v-for="recipe in recipes"
        :key="recipe.id"
      >
        <Recipe :recipe="recipe" />
      </div>
    </div>
  </div>
  <!-- <Footer /> -->
  <div class="conatiner-fluid">
    <div class="row justify-content-end">
      <div class="col-md-2">
        <i
          class="mdi mdi-plus bg-dark rounded-circle text-white f-24 selectable"
          data-bs-toggle="modal"
          data-bs-target="#modal"
        ></i>
      </div>
      <Modal>
        <template #modal-title>Add New Recipe</template>
        <template #modal-body>
          <AddRecipeForm />
        </template>
      </Modal>
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
