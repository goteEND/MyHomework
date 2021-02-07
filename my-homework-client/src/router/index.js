import Vue from "vue";
import VueRouter from "vue-router";

import Home from "../views/Home.vue";
import Auth from "../views/Authentication.vue";
import ProjectList from "../views/ProjectList.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/auth",
    name: "Authentication",
    component: Auth,
  },
  {
    path: "/subjects/:id",
    name: "Projects",
    component: ProjectList,
  },
];

const router = new VueRouter({
  mode: "history",
  routes,
});

export default router;
