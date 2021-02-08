<template>
  <v-container>
    <v-card class="mt-5">
      <v-toolbar color="purple">
        <v-toolbar-title>{{ subjectName }}</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn @click="$router.replace('/')">Back</v-btn>
      </v-toolbar>

      <v-dialog v-model="dialog" persistent max-width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            v-if="
              $store.getters.getLogedUser &&
                $store.getters.getLogedUser.role == 'Professor'
            "
            color="primary"
            dark
            v-bind="attrs"
            v-on="on"
          >
            Create New Project
          </v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">New Project</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    label="Project Name"
                    v-model="projectForCreateAndUpdate.name"
                    required
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    label="Project Description"
                    v-model="projectForCreateAndUpdate.description"
                    required
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-menu
                ref="showDatePicker"
                v-model="showDatePicker"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="auto"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="projectForCreateAndUpdate.dueDate"
                    label="Due date"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker
                  ref="picker"
                  v-model="projectForCreateAndUpdate.dueDate"
                  :max="new Date().toISOString().substr(0, 10)"
                  min="1950-01-01"
                  @change="setDate"
                ></v-date-picker>
              </v-menu>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              Close
            </v-btn>
            <v-btn color="blue darken-1" text @click="createProject">
              Save
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

      <project-card
        v-for="(project, index) in projects"
        :project="project"
        :key="index"
        @removeCard="removeCard"
        @getProjectsBySubjectId="getProjectsBySubjectId"
      />
    </v-card>
  </v-container>
</template>

<script>
import axios from 'axios'
import ProjectCard from '../components/ProjectCard'
export default {
  name: 'ProjectList',
  components: { ProjectCard },
  data: () => ({
    projects: [],
    subject: '',
    dialog: false,
    showDatePicker: false,
    projectForCreateAndUpdate: {
      name: null,
      description: null,
      dueDate: null,
      subjectId: null,
    },
  }),
  mounted() {
    this.getProjectsBySubjectId()
  },
  watch: {
    showDatePicker(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    },
  },
  computed: {
    subjectName() {
      return this.$store.getters.getSelectedSubjectName
    },
  },

  methods: {
    async getProjectsBySubjectId() {
      const response = await axios({
        method: 'get',
        url: `http://localhost:5000/api/subjects/${this.$route.params.id}/projects`,
        data: {},
        headers: {
          Authorization: 'Bearer ' + this.$store.getters.getToken,
        },
      })
      this.projects = response.data
    },

    removeCard(id) {
      this.projects = this.projects.filter(project => project.id != id)
    },
    setDate(date) {
      this.$refs.showDatePicker.save(date)
    },

    async createProject() {
      this.dialog = false

      this.projectForCreateAndUpdate.subjectId = this.$route.params.id

      const response = await axios({
        method: 'post',
        url: 'http://localhost:5000/api/projects',
        data: this.projectForCreateAndUpdate,
        headers: {
          Authorization: 'Bearer ' + this.$store.getters.getToken,
        },
      })

      if (response.status == 201) {
        this.projectForCreateAndUpdate = {
          name: null,
          description: null,
          dueDate: null,
          subjectId: null,
        }
        await this.getProjectsBySubjectId()
      }
    },
  },
}
</script>

<style></style>
