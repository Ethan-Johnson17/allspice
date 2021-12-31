import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class StepsService {
  async getAll(query = '') {
    const res = await api.get(query)
    AppState.steps = res.data
  }

  async addStep(recipeId, step) {
    const res = await api.post('api/recipes/' + recipeId + '/steps', step)
    logger.log('post?', res.data)
    AppState.steps.push(res.data)
  }
}
export const stepsService = new StepsService()