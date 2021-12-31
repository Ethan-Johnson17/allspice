<template>
  <div class="container modal-body">
    <div class="row">
      <div class="col-md-4">
        <img :src="recipe.imgUrl" alt="Recipe img" />
      </div>
      <div class="col-md-8">
        <div class="row mb-2">
          <div class="col-md-12 border-bottom border-dark">
            <h3 class="mb-0 pb-0">{{ recipe.title }}</h3>
            <h6 class="opacity-75 mt-0 pt-0">"{{ recipe.subtitle }}"</h6>
          </div>
        </div>
        <div class="row justify-content-start">
          <div class="col-md-5 me-3"><Steps :recipe="recipe" /></div>
          <div class="col-md-5 ms-2"><Ingredients :recipe="recipe" /></div>
        </div>
      </div>
    </div>

    <div class="row justify-content-end" v-if="recipe.creatorId == account.id">
      <div class="col-md-1">
        <i
          class="mdi mdi-delete-circle-outline f-20 selectable"
          title="delete"
          @click="remove(recipe.id)"
        ></i>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { recipesService } from '../services/RecipesService'
import { ingredientsService } from '../services/IngredientsService'
import Pop from '../utils/Pop'
import { Modal } from "bootstrap"
import { onMounted } from '@vue/runtime-core'

export default {
  props: { recipe: { type: Object } },
  setup(props) {
    const editable = ref({})
    return {
      editable,
      account: computed(() => AppState.account),

      async remove(id) {
        const yes = await Pop.confirm('Remove Recipe?')
        if (!yes) { return }
        Modal.getOrCreateInstance('#recipeDetails-' + id).toggle()
        await recipesService.remove(id)
      },


    }
  }
}
</script>


<style lang="scss" scoped>
img {
  object-fit: cover;
  height: 400px;
  width: 250px;
}
</style>