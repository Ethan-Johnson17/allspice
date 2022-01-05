<template>
  <div @click="openModal(recipe.id)" :id="'#recipeDetails-' + recipe.id">
    <div
      class="body rounded selectGrow"
      :style="{
        'background-image': 'url(' + recipe.imgUrl + ')',
        height: '300px',
      }"
    >
      <div class="recipe container img-fluid rounded footer">
        <div class="row pt-1 footer">
          <div class="col-md-6 mx-0 mt pe-0">
            <p class="bg-dark rounded px-2">{{ recipe.category }}</p>
          </div>
          <div class="col-md-2 mx-0 px-0 d-flex ms-auto">
            <div
              class="f-16 text-dark me-1"
              title="Favorite"
              v-if="!isFavorite"
            >
              <i
                class="mdi mdi-heart selectable"
                @click.stop="addFavorite(recipe.id, account.id)"
              ></i>
            </div>
            <div class="f-16 text-dark me-1" title="Favorite" v-if="isFavorite">
              <i
                class="mdi mdi-heart selectable text-danger"
                @click.stop="addFavorite(recipe.id, account.id)"
              ></i>
            </div>
            <div class="f-16 text-dark" title="Try">
              <i
                class="mdi mdi-bookmark selectable"
                @click.stop="addTry(recipe.id)"
              ></i>
            </div>
          </div>
        </div>
      </div>
      <div class="container rounded px-0">
        <div class="row mt-auto bg-dark opacity-50 rounded mx-0 px-0">
          <div class="col-md-12">{{ recipe.title }}</div>
          <div class="col-md-12 mb-1">{{ recipe.subtitle }}</div>
        </div>
      </div>
    </div>
  </div>
  <Modal :id="'recipeDetails-' + recipe.id">
    <template #modal-title>Recipe Details</template>
    <template #modal-body>
      <RecipeDetailsForm :recipe="recipe" />
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger'
import { accountService } from '../services/AccountService'
import { onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
export default {
  props: {
    recipe: { type: Object },
  },
  setup(props) {
    // onMounted(async () => {
    //   try {
    //     await accountService.getFavorites('account/favorites')
    //   } catch (error) {
    //     logger.error(error)
    //     Pop.toast(error.message, 'error')
    //   }
    // })
    return {
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      isFavorite: computed(() => AppState.favorites.find(r => r.id == props.recipe.id)),

      openModal(id) {
        Modal.getOrCreateInstance('#recipeDetails-' + id).toggle()
      },

      async addFavorite(recipeid, accountId) {
        let favorite = { recipeId: recipeid, accountId: accountId }
        await accountService.createFavorite(accountId, favorite)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.footer {
  flex-grow: 1;
}

.body {
  display: flex;
  flex-direction: column;
  background-size: cover;
}

.selectGrow {
  transition: 500ms;
}

.selectGrow:hover {
  transform: scale(1.05);
  transition: 500ms;
}

a {
  text-decoration: none;
}
</style>