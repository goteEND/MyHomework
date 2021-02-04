export default {
  setUser(state, payload) {
    state.userId = payload.userId;
    state.userName = payload.userName;
    state.token = payload.token;
  },
  setAuthPage(state, payload) {
    state.authPage = payload;
  },
};
