<template>
  <v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-slide-y-transition>
          <v-card elevation="3" key="pageToShow">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Authentication</v-toolbar-title>
              <v-spacer></v-spacer>
            </v-toolbar>
            <component :is="pageToShow" />
          </v-card>
        </v-slide-y-transition>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import login from "../components/Login";
import register from "../components/Register";
export default {
  name: "Authentication",
  components: { login, register },
  data: () => ({}),
  computed: {
    pageToShow() {
      return this.$store.getters.getAuthPage;
    },
  },
};
</script>

<style scoped>
.auth-enter-from,
.auth-leave-to {
  opacity: 0;
}
.base-enter-active {
  transition: all 0.5s ease-in;
}
.base-leave-active {
  transition: all 0.5s ease-out;
}
.auth-enter-to,
.auth-leave-from {
  opacity: 0;
}
</style>
