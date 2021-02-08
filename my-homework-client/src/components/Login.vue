<template>
  <v-container>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-alert
        v-if="alert"
        dense
        outlined
        type="error"
        dismissible
        transition="scale-transition"
      >
        {{ error }}
      </v-alert>

      <v-text-field
        v-model="username"
        :rules="usernameRules"
        label="Username"
        required
      ></v-text-field>

      <v-text-field
        v-model="password"
        :rules="passwordRules"
        type="password"
        label="Password"
      >
      </v-text-field>

      <v-btn
        :disabled="!valid"
        :loading="isLoading"
        color="success"
        class="mr-4"
        @click="validate"
      >
        Login
      </v-btn>

      <v-btn text @click="changePage">
        Register
      </v-btn>
    </v-form>
  </v-container>
</template>

<script>
export default {
  name: 'Login',
  data: () => ({
    isLoading: false,
    error: '',
    alert: false,
    valid: true,
    username: '',
    usernameRules: [v => !!v || 'Username is required'],
    password: '',
    passwordRules: [v => !!v || 'Password is required'],
  }),
  methods: {
    validate() {
      this.$refs.form.validate()
      this.signin()
    },
    async signin() {
      this.isLoading = true
      try {
        await this.$store.dispatch('signin', {
          username: this.username,
          password: this.password,
        })
      } catch (err) {
        this.alert = true
        this.error = 'Failed to authenticate'
      }
      this.isLoading = false
    },
    changePage() {
      ;(this.username = ''),
        (this.password = ''),
        this.$store.dispatch('authPage', 'register')
    },
  },
}
</script>

<style scoped></style>
