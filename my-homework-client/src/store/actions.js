import axios from 'axios'
import router from '../router/index'
import VueJwtDecode from 'vue-jwt-decode'

export default {
  async signin(context, payload) {
    const response = await axios({
      method: 'post',
      url: 'http://localhost:5000/api/Auth/authentication',
      data: {
        username: payload.username,
        password: payload.password,
      },
      headers: {},
    })

    const responseData = response.data

    if (response.status == 200) {
      context.commit('setUser', {
        logedUser: {
          userId: responseData.id,
          userName: responseData.username,
          role: VueJwtDecode.decode(responseData.token).role,
        },
        token: responseData.token,
      })
      localStorage.setItem('logedUser', {
        userId: responseData.id,
        userName: responseData.username,
        role: VueJwtDecode.decode(responseData.token).role,
      })
      localStorage.setItem('token', responseData.token)
      router.replace('/')
    } else {
      const error = new Error(responseData.message || 'Failed to authenticate')
      throw error
    }
  },
  async signup({ dispatch }, payload) {
    const response = await axios({
      method: 'post',
      url: 'http://localhost:5000/api/Auth/registration',
      data: {
        firstName: payload.firstName,
        lastName: payload.lastName,
        password: payload.password,
        email: payload.email,
      },
      headers: {},
    })

    const responseData = response
    if (responseData.ok) {
      dispatch('authPage', 'login')
    } else {
      const error = new Error(
        responseData.message || 'Failed to register your account',
      )
      throw error
    }
  },
  trySignIn(context) {
    const token = localStorage.getItem('token')
    const logedUser = localStorage.getItem('logedUser')

    if (token && logedUser) {
      context.commit('setUser', { logedUser, token })
    }
  },
  signOut(context, payload) {
    context.commit('setUser', {
      logedUser: payload,
      token: payload,
    })
    localStorage.removeItem('logedUser')
    localStorage.removeItem('token')
    router.replace('/auth')
  },
  authPage(context, payload) {
    context.commit('setAuthPage', payload)
  },
  selectedSubjectName(context, payload) {
    context.commit('setSelectedSubjectName', payload)
  },
}
