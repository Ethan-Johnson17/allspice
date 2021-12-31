<template>
  <div class="card row">
    <div class="card-header">Steps</div>
    <div class="card-body col">
      <h5 class="card-title">Steps</h5>
      <div class="row" v-for="step in steps" :key="step.id">
        <div class="col-md-8">
          <p>{{ step.bodyText }}</p>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <form @submit.prevent="addStep">
            <input
              type="text"
              aria-label="Add Step"
              placeholder="Add Step"
              class="form-control-sm"
              v-model="editable.bodyText"
              required
            />
            <input
              type="number"
              aria-label="Step Order"
              placeholder="Step Order"
              class="form-control-sm"
              v-model="editable.stepOrder"
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
import { onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { stepsService } from '../services/StepsService'

export default {
  props: { recipe: { type: Object } },
  setup(props) {
    const editable = ref({})
    onMounted(async () => {
      try {
        await stepsService.getAll('api/recipes/' + props.recipe.id + '/steps')
      } catch (error) {
        logger.log(error)
        Pop.error(error.message, 'error')
      }
    })
    return {
      editable,
      steps: computed(() => AppState.steps.filter(s => s.recipeId == props.recipe.id)),

      async addStep() {
        const step = editable.value
        step.recipeId = props.recipe.id
        const recipeId = props.recipe.id
        await stepsService.addStep(recipeId, step)
        editable.value = {}
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>