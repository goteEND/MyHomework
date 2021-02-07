<template>
  <v-container>
    <v-card @click="merge">{{ subject.name }}</v-card>
  </v-container>
</template>

<script>
export default {
  name: 'SubjectCard',
  props: ['subject'],
  data: () => ({}),
  methods: {
    merge() {
      this.$store.dispatch('selectedSubjectName', this.subject.name)
      this.$router.push(`/subjects/${this.subject.id}`)
    },
  },
}
</script>
