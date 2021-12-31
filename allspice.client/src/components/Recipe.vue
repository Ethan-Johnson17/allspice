<template>
  <a data-bs-toggle="modal" :href="'#recipeDetails-' + recipe.id">
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
          <div class="col-md-2 mx-0 px-0"></div>
          <div class="col-md-1 selectable me-1 px-1 f-16" title="Favorite">
            <i class="mdi mdi-heart"></i>
          </div>
          <div class="col-md-1 selectable px-1 f-16" title="Try">
            <i class="mdi mdi-bookmark"></i>
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
  </a>
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
export default {
  props: {
    recipe: { type: Object },
  },
  setup(props) {
    return {
      recipes: computed(() => AppState.recipes),
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