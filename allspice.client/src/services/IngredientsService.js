import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class IngredientsService {
  async getAll(query = '') {
    const res = await api.get(query)
    AppState.ingredients = res.data
  }

  async addIngredient(recipeId, ingredient) {
    const res = await api.post('api/recipes/' + recipeId + '/ingredients', ingredient)
    AppState.ingredients.push(res.data)
  }
}

export const ingredientsService = new IngredientsService()