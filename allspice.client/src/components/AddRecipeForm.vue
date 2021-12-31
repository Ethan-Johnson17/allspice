<template>
  <form @submit.prevent="addRecipe">
    <div class="modal-body">
      <div class="row">
        <div class="col-md-7 p-0">
          <h6>Recipe Title</h6>
          <input
            type="text"
            aria-label="Recipe title"
            placeholder="Recipe title..."
            class="form-control"
            v-model="editable.title"
            required
          />
        </div>
        <div class="col-md-4 ms-2">
          <div class="dropdown mt-4 pt-1 px-0">
            <button
              class="btn btn-success dropdown-toggle"
              type="button"
              id="dropdownMenuButton1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
              required
            >
              {{ category }}
            </button>
            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
              <li
                class="dropdown-item selectable"
                @click="category = 'Appetizer'"
              >
                Appetizer
              </li>
              <li
                class="dropdown-item selectable"
                @click="category = 'Breakfast'"
              >
                Breakfast
              </li>
              <li class="dropdown-item selectable" @click="category = 'Entree'">
                Entree
              </li>
              <li class="dropdown-item selectable" @click="category = 'Side'">
                Side
              </li>
              <li
                class="dropdown-item selectable"
                @click="category = 'Dessert'"
              >
                Dessert
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-10 p-0">
          <h6>Recipe subtitle</h6>
          <input
            type="text"
            aria-label="Subtitle"
            placeholder="Catchy subtitle..."
            class="form-control"
            v-model="editable.subtitle"
            required
          />
        </div>
      </div>
      <div class="row">
        <div class="col-md-10 p-0">
          <h6>Recipe Image</h6>
          <input
            type="url"
            aria-label="Image Url"
            placeholder="Image Url"
            class="form-control"
            v-model="editable.imgUrl"
            required
          />
        </div>
      </div>
      <div class="row"></div>
    </div>
    <div class="modal-footer">
      <button
        type="button"
        class="btn btn-outline-secondary"
        data-bs-dismiss="modal"
      >
        Close
      </button>
      <button
        type="submit"
        class="btn btn-outline-success"
        data-bs-dismiss="modal"
      >
        Create Recipe
      </button>
    </div>
  </form>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { Modal } from "bootstrap"
import { AppState } from "../AppState"
import { recipesService } from '../services/RecipesService'
export default {
  setup() {
    const editable = ref({})
    const category = ref('Choose Category')

    return {
      editable,
      category,

      async addRecipe() {
        try {
          const recipe = editable.value
          recipe.category = category.value
          logger.log('Create', recipe)
          await recipesService.createRecipe(recipe)
          editable.value = {}
          category.value = 'Choose Category'
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>