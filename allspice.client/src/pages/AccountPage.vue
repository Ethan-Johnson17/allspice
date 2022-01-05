<template>
  <div class="container">
    <div class="about text-center">
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
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
  <div class="container-fluid">
    <div class="row justify-content-end">
      <div class="col-md-2 text-center f-24">
        <i
          class="mdi mdi-plus bg-dark rounded-circle text-white selectable"
          data-bs-toggle="modal"
          data-bs-target="#modal"
        ></i>
      </div>
      <FormModal>
        <template #modal-title>Add New Recipe</template>
        <template #modal-body>
          <AddRecipeForm />
        </template>
      </FormModal>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'

import Pop from '../utils/Pop'
export default {
  props: {
    recipe: { type: Object },
  },
  name: 'Account',
  setup(props) {
    onMounted(async () => {
      try {
        await accountService.getFavorites('account/favorites')
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      recipes: computed(() => AppState.recipes),
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
