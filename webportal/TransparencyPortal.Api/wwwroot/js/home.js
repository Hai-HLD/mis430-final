(function () {
  document.addEventListener("DOMContentLoaded", function () {
    if (typeof TPAuth === "undefined" || !TPAuth.isLoggedIn()) {
      location.replace("login.html");
      return;
    }

    var nameEl = document.getElementById("home-username");
    if (nameEl) {
      nameEl.textContent = TPAuth.getUsername() || "User";
    }

    var logoutBtn = document.getElementById("home-logout");
    if (logoutBtn) {
      logoutBtn.addEventListener("click", function () {
        TPAuth.logout();
      });
    }
  });
})();
