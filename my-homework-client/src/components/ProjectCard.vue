<template>
  <v-card class="ma-5">
    <v-toolbar color="purple">
      <v-toolbar-title>{{ project.name }}</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <div class="text-body-1">{{ project.description }}</div>
    <div class="text-body-1">
      {{ project.dueDate ? displayDate() : 'No Due Date' }}
    </div>
    <div class="text-body-1">
      Github link: {{ project.githubLink ? project.githubLink : 'Not set' }}
    </div>
    <div class="text-body-1">
      {{
        project.enrolledStudent
          ? 'Enrolled student: ' +
            project.enrolledStudent.firstName +
            ' ' +
            project.enrolledStudent.lastName
          : 'No student enrolled yet.'
      }}
    </div>

    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          v-if="
            $store.getters.getLogedUser &&
              $store.getters.getLogedUser.role == 'Student'
          "
          v-bind="attrs"
          v-on="on"
          color="primary"
          :disabled="project.enrolledStudent ? true : false"
        >
          Enroll
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">Enroll in project: {{ project.name }}</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field
                  label="GitHub Link*"
                  v-model="enrollGitHub"
                  required
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false">
            Close
          </v-btn>
          <v-btn color="blue darken-1" text @click="enroll">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-btn
      v-if="
        $store.getters.getLogedUser &&
          $store.getters.getLogedUser.role == 'Professor'
      "
      color="primary"
      @click="deleteProject"
    >
      Delete
    </v-btn>

    <v-dialog
      v-if="
        $store.getters.getLogedUser &&
          $store.getters.getLogedUser.role == 'Professor'
      "
      v-model="dialog"
      persistent
      max-width="600px"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          Edit Project
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">{{ project.name }}</span>
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
          <v-btn color="blue darken-1" text @click="updateProject">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<script>
import axios from 'axios'
import moment from 'moment'
export default {
  name: 'ProjectCard',
  data: () => ({
    enrollGitHub: null,
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
    if (this.project.dueDate) {
      this.project.dueDate = this.project.dueDate.split('T')[0]
    }

    this.projectForCreateAndUpdate.name = this.project.name
    this.projectForCreateAndUpdate.description = this.project.description
    this.projectForCreateAndUpdate.dueDate = this.project.dueDate
    this.projectForCreateAndUpdate.subjectId = this.$route.params.id
  },
  props: ['project'],
  emits: ['removeCard'],
  watch: {
    showDatePicker(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    },
  },
  methods: {
    setDate(date) {
      this.$refs.showDatePicker.save(date)
    },
    async deleteProject() {
      const response = await axios({
        method: 'delete',
        url: `http://localhost:5000/api/projects/${this.project.id}`,
        data: {},
        headers: {
          Authorization: 'Bearer ' + this.$store.getters.getToken,
        },
      })

      if (response.status == 204) {
        this.$emit('removeCard', this.project.id)
      }
    },
    async updateProject() {
      this.dialog = false

      // update project
      this.project.name = this.projectForCreateAndUpdate.name
      this.project.description = this.projectForCreateAndUpdate.description
      this.project.dueDate = this.projectForCreateAndUpdate.dueDate

      if (this.project.enrolledStudent != null) {
        this.project['enrolledStudentId'] = this.project.enrolledStudent.id
      }

      // upp
      await axios({
        method: 'put',
        url: `http://localhost:5000/api/projects/${this.project.id}`,
        data: this.project,
        headers: {
          Authorization: 'Bearer ' + this.$store.getters.getToken,
        },
      })
    },
    displayDate() {
      return 'Due Date: ' + moment(this.project.dueDate).format('YYYY-MM-DD')
    },
    async enroll() {
      this.dialog = false
      const response = await axios({
        method: 'patch',
        url: `http://localhost:5000/api/Projects/${this.project.id}/enrolledStudent/${this.$store.getters.getLogedUser.userId}`,
        data: {
          githubLink: this.enrollGitHub,
        },
        headers: {
          Authorization: 'Bearer ' + this.$store.getters.getToken,
        },
      })
      if (response.status == 200) {
        this.$emit('getProjectsBySubjectId')
      }
    },
  },
}
</script>

<style></style>
