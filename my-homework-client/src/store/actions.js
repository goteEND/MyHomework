import axios from "axios";

export default {
  async signin(context, payload) {
    const response = await axios.post(
      "http://localhost:5000/api/Auth/authentication",
      {
        username: payload.username,
        password: payload.password,
      }
    );
    console.log(response);
    const responseData = response;
    if (responseData.ok) {
      context.commit("setUser", {
        userId: response.userId,
        userName: response.userName,
        token: response.token,
      });
    } else {
      const error = new Error(responseData.message || "Failed to authenticate");
      throw error;
    }
  },
  async signup({ dispatch }, payload) {
    const response = await axios.post(
      "http://localhost:5000/api/Auth/registration",
      {
        firstName: payload.firstName,
        lastName: payload.lastName,
        password: payload.password,
        email: payload.email,
      }
    );
    const responseData = response;
    if (!responseData.ok) {
      const error = new Error(
        responseData.message || "Failed to register your account"
      );
      throw error;
    } else {
      dispatch("authPage", "login");
    }
  },
  authPage(context, payload) {
    context.commit("setAuthPage", payload);
  },
};
