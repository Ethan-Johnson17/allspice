import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {
  async getAll(query = '') {
    logger.log("recipes sent")
    const res = await api.get(query)
    logger.log("recipes", res.data)
    AppState.recipes = res.data
  }
}

export const recipesService = new RecipesService()