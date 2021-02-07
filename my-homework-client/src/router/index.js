import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store/index'

import Home from '../views/Home.vue'
import Auth from '../views/Authentication.vue'
import ProjectList from '../views/ProjectList.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    meta: { requiresAuth: true },
    beforeEnter(to, from, next) {
      if (store.getters.isAuthenticated) {
        next()
      } else {
        next({
          name: 'Authentication',
        })
      }
    },
  },
  {
    path: '/auth',
    name: 'Authentication',
    component: Auth,
    meta: { requiresUnauth: true },
    beforeEnter(to, from, next) {
      if (!store.getters.isAuthenticated) {
        next()
      } else {
        next({
          name: 'Home',
        })
      }
    },
  },
  {
    path: '/subjects/:id',
    name: 'Projects',
    component: ProjectList,
    beforeEnter(to, from, next) {
      if (store.getters.isAuthenticated) {
        next()
      } else {
        next({
          name: 'Authentication',
        })
      }
    },
  },
]

const router = new VueRouter({
  mode: 'history',
  routes,
})

export default router
