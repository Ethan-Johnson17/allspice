<template>
  <div class="card row">
    <div class="card-header">Ingredients</div>
    <div class="card-body col">
      <h5 class="card-title">Ingredients</h5>
      <div class="row" v-for="ingredient in ingredients" :key="ingredient.id">
        <div class="col-md-8 d-flex">
          <p
            class="px-1 me-1"
            :contenteditable="
              ingredient.creatorId === account.id ? 'true' : 'false'
            "
            @blur="editIngredient(ingredient.id)"
          >
            {{ ingredient.quantity }}
          </p>
          <p
            class="px-1 me-1"
            :contenteditable="
              ingredient.creatorId === account.id ? 'true' : 'false'
            "
            @blur="editIngredient(ingredient.id)"
          >
            {{ ingredient.inName }}
          </p>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <form @submit.prevent="addIngredient">
            <input
              type="text"
              aria-label="Add Ingredient"
              placeholder="Add Ingredient"
              class="form-control-sm"
              v-model="editable.inName"
              required
            />
            <input
              type="text"
              aria-label="Quantity"
              placeholder="Quantity"
              class="form-control-sm"
              v-model="editable.quantity"
              required
            />
            <button type="submit" class="btn btn-success ms-3 py-1">Add</button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { ingredientsService } from '../services/IngredientsService'
export default {
  props: { recipe: { type: Object } },
  setup(props) {
    const editable = ref({})
    onMounted(async () => {
      try {
        await ingredientsService.getAll('api/recipes/' + props.recipe.id + '/ingredients')
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      editable,
      ingredients: computed(() => AppState.ingredients.filter(i => i.recipeId == props.recipe.id)),
      account: computed(() => AppState.account),


      async addIngredient() {
        const ingredients = editable.value
        ingredients.recipeId = props.recipe.id
        const recipeId = props.recipe.id
        logger.log('ingred', ingredients)
        await ingredientsService.addIngredient(recipeId, ingredients)
        editable.value = {}
      },

      async editIngredient(id) {
        try {
          let data = window.event.target.innerText
          let ingredient = { bodyText: data }
          logger.log('controller', data)
          await ingredientsService.editIngredient(id, ingredient)
          await ingredientsService.getAll('api/recipes/' + props.recipe.id + '/ingredients')
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
</style>