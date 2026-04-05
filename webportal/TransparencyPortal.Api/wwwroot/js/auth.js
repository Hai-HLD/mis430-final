(function (global) {
  const SESSION_KEY = "tp_session";
  const USER_KEY = "tp_username";
  const POST_LOGIN_KEY = "tp_post_login";

  global.TPAuth = {
    SESSION_KEY: SESSION_KEY,
    USER_KEY: USER_KEY,
    POST_LOGIN_KEY: POST_LOGIN_KEY,

    isLoggedIn: function () {
      return sessionStorage.getItem(SESSION_KEY) === "1";
    },

    getUsername: function () {
      return sessionStorage.getItem(USER_KEY) || "";
    },

    setSession: function (username) {
      sessionStorage.setItem(SESSION_KEY, "1");
      sessionStorage.setItem(USER_KEY, username);
    },

    clearSession: function () {
      sessionStorage.removeItem(SESSION_KEY);
      sessionStorage.removeItem(USER_KEY);
    },

    logout: function () {
      this.clearSession();
      global.location.href = "login.html";
    },

    redirectToLogin: function () {
      try {
        sessionStorage.setItem(POST_LOGIN_KEY, global.location.href);
      } catch (e) {
        /* ignore */
      }
      global.location.replace("login.html");
    },

    /** Returns saved URL or defaultPath (e.g. home.html). Clears the stored value. */
    consumePostLoginUrl: function (defaultPath) {
      var u = sessionStorage.getItem(POST_LOGIN_KEY);
      sessionStorage.removeItem(POST_LOGIN_KEY);
      if (u && typeof u === "string") return u;
      return defaultPath || "home.html";
    },
  };
})(window);
