<template>
  <v-container fluid>
    <v-row justify="center">
      <v-col cols="12" sm="10" md="10">
        <v-card class="mt-5" elevation="3" key="pageToShow">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Subjects</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn @click="logout">Logout</v-btn>
          </v-toolbar>
          <subject-card
            v-for="(subject, index) in subjects"
            :subject="subject"
            :key="index"
          />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
// @ is an alias to /src
import axios from 'axios'
import subjectCard from '../components/SubjectCard'

export default {
  name: 'Home',
  components: { subjectCard },
  data: () => ({
    subjects: [],
  }),
  mounted() {
    this.getAllSubjects()
  },
  methods: {
    async getAllSubjects() {
      const response = await axios({
        method: 'get',
        url: 'http://localhost:5000/api/subjects',
        data: {},
        headers: {
          Authorization: 'Bearer ' + this.$store.getters.getToken,
        },
      })

      this.subjects = response.data
    },
    logout() {
      this.$store.dispatch('signOut', null)
    },
  },
}
</script>
