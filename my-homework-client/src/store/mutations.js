export default {
  setUser(state, payload) {
    state.logedUser = payload.logedUser
    state.token = payload.token
  },
  setAuthPage(state, payload) {
    state.authPage = payload
  },
  setSelectedSubjectName(state, payload) {
    state.selectedSubjectName = payload
  },
}
