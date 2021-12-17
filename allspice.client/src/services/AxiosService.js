import Axios from 'axios'
import { baseURL } from '../env'
export const api = Axios.create({
  baseURL,
  timeout: 8000
})

export const allSpiceApi = Axios.create({
  baseURL: 'https://allspice-api/herokuapp.com/api'
})
