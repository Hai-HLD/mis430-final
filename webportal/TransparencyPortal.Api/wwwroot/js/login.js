(function () {
  const DEMO_USER = "TestUser";
  const DEMO_PASSWORD = "testing";

  document.addEventListener("DOMContentLoaded", function () {
    if (typeof TPAuth !== "undefined" && TPAuth.isLoggedIn()) {
      location.replace("home.html");
      return;
    }

    const form = document.getElementById("login-form");
    const username = document.getElementById("login-username");
    const password = document.getElementById("login-password");
    const errorEl = document.getElementById("login-error");

    form.addEventListener("submit", function (e) {
      e.preventDefault();
      errorEl.hidden = true;

      const u = username.value.trim();
      const p = password.value;

      if (u === DEMO_USER && p === DEMO_PASSWORD) {
        TPAuth.setSession(DEMO_USER);
        var next = TPAuth.consumePostLoginUrl("home.html");
        location.href = next;
        return;
      }

      errorEl.hidden = false;
      password.value = "";
      password.focus();
    });
  });
})();
