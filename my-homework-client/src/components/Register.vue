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
        v-model="firstName"
        :rules="firstNameRules"
        label="First Name"
        required
      ></v-text-field>

      <v-text-field
        v-model="lastName"
        :rules="lastNameRules"
        label="Last Name"
        required
      ></v-text-field>

      <v-text-field
        v-model="email"
        :rules="emailRules"
        label="E-mail"
        required
      ></v-text-field>

      <v-text-field
        v-model="password"
        counter
        :rules="passwordRules"
        type="password"
        hint="At least 5 characters"
        label="Password"
      >
      </v-text-field>

      <p v-if="!!firstName || !!lastname">
        Your username is {{ firstName }}{{ lastName }}
      </p>

      <v-btn
        :disabled="!valid"
        :loading="isLoading"
        color="success"
        class="mr-4"
        @click="validate"
      >
        Register
      </v-btn>

      <v-btn text @click="changePage">
        Login
      </v-btn>
    </v-form>
  </v-container>
</template>

<script>
export default {
  name: 'Register',
  data: () => ({
    isLoading: false,
    error: '',
    alert: false,
    valid: true,
    firstName: '',
    firstNameRules: [v => !!v || 'First name is required'],
    lastName: '',
    lastNameRules: [v => !!v || 'Last name is required'],
    email: '',
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
    ],
    password: '',
    passwordRules: [
      v => !!v || 'Password is required',
      v => (v && v.length >= 5) || 'Password must be bigger than 5 characters',
    ],
  }),
  methods: {
    validate() {
      this.$refs.form.validate()
      this.register()
    },
    async register() {
      this.isLoading = true
      try {
        await this.$store.dispatch('signup', {
          firstName: this.firstName,
          lastName: this.lastName,
          email: this.email,
          password: this.password,
        })
      } catch (err) {
        this.error = 'Failed to register'
      }
      this.isLoading = false
    },
    changePage() {
      ;(this.firstName = ''),
        (this.lastName = ''),
        (this.email = ''),
        (this.password = ''),
        this.$store.dispatch('authPage', 'login')
    },
  },
}
</script>

<style></style>
