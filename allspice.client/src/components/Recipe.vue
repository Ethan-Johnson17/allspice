<template>
  <div
    class="body"
    style="
      background-image: url(https://thiscatdoesnotexist.com);
      height: 250px;
      width: 250px;
      object-fit: cover;
    "
  >
    <div class="recipe vh-50 container img-fluid rounded footer">
      <div class="row pt-1 footer">
        <div class="col-md-5 mx-0 mt pe-0">
          <p class="bg-dark rounded">Category</p>
        </div>
        <div class="col-md-4 mx-0 px-0"></div>
        <div class="col-md-1 selectable me-1 px-1 f-16" title="Favorite">
          <i class="mdi mdi-heart"></i>
        </div>
        <div class="col-md-1 selectable ms-1 px-1 f-16" title="Try">
          <i class="mdi mdi-bookmark"></i>
        </div>
      </div>
    </div>
    <div class="row mt-auto">
      <div class="col-md-12">{{ recipe.title }}</div>
      <div class="col-md-12">Subtitle</div>
    </div>
  </div>
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
</style>