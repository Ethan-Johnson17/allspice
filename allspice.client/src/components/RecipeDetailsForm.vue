<template>
  <div class="container modal-body p-0">
    <div class="row">
      <div
        class="col-md-4 rounded"
        :style="{
          'background-image': 'url(' + recipe.imgUrl + ')',
          height: '70vh',
        }"
      ></div>
      <div class="col-md-8 pt-3">
        <div class="row mb-2">
          <div class="col-md-9 border-bottom border-dark">
            <h3
              class="mb-0 pb-0"
              :contenteditable="
                recipe.creatorId === account.id ? 'true' : 'false'
              "
              @blur="editTitle(recipe.id)"
            >
              {{ recipe.title }}
            </h3>

            <h6
              class="opacity-75 mt-0 pt-0"
              :contenteditable="
                recipe.creatorId === account.id ? 'true' : 'false'
              "
              @blur="editSubtitle(recipe.id)"
            >
              {{ recipe.subtitle }}
            </h6>
          </div>
          <div class="col-md-2 pt-2">
            <h6 class="opacity-75 border border-dark rounded text-center">
              {{ recipe.category }}
            </h6>
          </div>
          <div class="col-md-1">
            <button
              type="button"
              class="btn-close mt-1"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
        </div>
        <div class="row justify-content-start">
          <div class="col-md-5 me-3"><Steps :recipe="recipe" /></div>
          <div class="col-md-5 ms-2"><Ingredients :recipe="recipe" /></div>
          <div
            class="col-md-11 text-end ms-4"
            v-if="recipe.creatorId == account.id"
          >
            <i
              class="mdi mdi-delete-circle-outline f-20 selectable"
              title="delete"
              @click="remove(recipe.id)"
            ></i>
          </div>
        </div>
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

      async editTitle(id) {
        try {
          let data = window.event.target.innerText
          let editedRecipe = { title: data }
          logger.log('controller', data)
          await recipesService.editRecipe(id, editedRecipe)
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, 'error')
        }
      },
      async editSubtitle(id) {
        try {
          let data = window.event.target.innerText
          let editedRecipe = { subtitle: data }
          logger.log('controller', data)
          await recipesService.editRecipe(id, editedRecipe)
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
div {
  background-size: cover;
}
</style>