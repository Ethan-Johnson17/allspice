import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async createFavorite(accountId, favorite) {
    try {
      const res = await api.post('/account/favorites', favorite)
      AppState.favorites = res.data
      logger.log('fff', res.data)
    } catch (error) {
      logger.error('Add favorite failed', error)

    }
  }

  async getFavorites(query = '') {
    const res = await api.get(query)
    AppState.favorites = res.data
    logger.log('service', res.data)
  }
}

export const accountService = new AccountService()
