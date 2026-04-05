(function () {
  if (typeof TPAuth === "undefined") {
    return;
  }
  if (!TPAuth.isLoggedIn()) {
    TPAuth.redirectToLogin();
  }
})();
