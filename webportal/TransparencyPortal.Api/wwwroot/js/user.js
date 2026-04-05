(function () {
  const STORAGE_KEY = "transparency_portal_tickets_v1";
  const FLASH_KEY = "tp_inquiry_flash";
  const PAGE_TITLE = "Transparency Portal — Submit an inquiry";

  /** Shown as the first support reply in every ticket thread (demo placeholder). */
  const STAFF_PLACEHOLDER_RESPONSE =
    "Thank you for reaching out through the Transparency Portal. We’ve received your inquiry and a member of our support team will review it. " +
    "If your question relates to published transparency materials or product documentation, we may direct you to those resources in a future message. " +
    "We appreciate your patience.";

  const formPanel = document.getElementById("form-panel");
  const ticketForm = document.getElementById("ticket-form");
  const historyPanel = document.getElementById("history-panel");
  const ticketHistory = document.getElementById("ticket-history");

  const ticketDetailPanel = document.getElementById("ticket-detail-panel");
  const ticketDetailFlash = document.getElementById("ticket-detail-flash");
  const ticketNotFound = document.getElementById("ticket-not-found");
  const notFoundId = document.getElementById("not-found-id");
  const ticketDetailContent = document.getElementById("ticket-detail-content");
  const detailTicketId = document.getElementById("detail-ticket-id");
  const detailCreated = document.getElementById("detail-created");
  const detailCategory = document.getElementById("detail-category");
  const detailSubject = document.getElementById("detail-subject");
  const detailContactLine = document.getElementById("detail-contact-line");
  const conversationThread = document.getElementById("conversation-thread");
  const followupForm = document.getElementById("followup-form");
  const followupMessage = document.getElementById("followup-message");
  const btnTicketBack = document.getElementById("btn-ticket-back");
  const btnTicketAnother = document.getElementById("btn-ticket-another");
  const linkNotFoundHome = document.getElementById("link-not-found-home");

  const category = document.getElementById("category");
  const subject = document.getElementById("subject");
  const message = document.getElementById("message");
  const contactName = document.getElementById("contact-name");
  const contactEmail = document.getElementById("contact-email");

  let currentDetailTicketId = null;

  function loadTickets() {
    try {
      const raw = localStorage.getItem(STORAGE_KEY);
      if (!raw) return [];
      const parsed = JSON.parse(raw);
      return Array.isArray(parsed) ? parsed : [];
    } catch {
      return [];
    }
  }

  function persistTickets(list) {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(list));
  }

  function findTicket(id) {
    return loadTickets().find(function (t) {
      return t.id === id;
    });
  }

  function saveTicket(record) {
    const list = loadTickets();
    list.unshift(record);
    const trimmed = list.slice(0, 25);
    persistTickets(trimmed);
    return trimmed;
  }

  function ensureFollowUps(t) {
    if (!Array.isArray(t.followUps)) t.followUps = [];
    return t;
  }

  function appendFollowUp(ticketId, body) {
    const list = loadTickets();
    const idx = list.findIndex(function (x) {
      return x.id === ticketId;
    });
    if (idx === -1) return false;
    const t = ensureFollowUps(list[idx]);
    t.followUps.push({
      at: new Date().toISOString(),
      body: body,
    });
    list[idx] = t;
    persistTickets(list);
    return true;
  }

  function formatCategory(value) {
    const labels = {
      transparency: "Transparency & policy",
      ranking: "Ranking, search & prioritization",
      technical: "Technical issue",
      account: "Account access",
      billing: "Billing",
      feedback: "Feedback",
      other: "Other",
    };
    return labels[value] || value;
  }

  function formatWhen(iso) {
    if (!iso) return "—";
    try {
      return new Date(iso).toLocaleString();
    } catch {
      return "—";
    }
  }

  /** Slightly after the user message so the thread reads in order. */
  function offsetIso(iso, seconds) {
    if (!iso) return iso;
    try {
      const d = new Date(iso);
      d.setSeconds(d.getSeconds() + seconds);
      return d.toISOString();
    } catch {
      return iso;
    }
  }

  function parseTicketIdFromHash() {
    const m = /^#ticket\/(\d{8})$/.exec(location.hash);
    return m ? m[1] : null;
  }

  function goHome() {
    if (location.hash) {
      const url = location.href.split("#")[0];
      history.replaceState(null, "", url);
    }
    route();
  }

  function showHomeChrome() {
    currentDetailTicketId = null;
    formPanel.hidden = false;
    ticketDetailPanel.hidden = true;
    renderHistory();
    document.title = PAGE_TITLE;
  }

  function bubbleHtml(role, label, timeIso, bodyText) {
    const roleClass = role === "staff" ? "bubble--staff" : "bubble--user";
    const timeStr = formatWhen(timeIso);
    return (
      '<div class="bubble ' +
      roleClass +
      '">' +
      '<div class="bubble-head">' +
      '<span class="bubble-label">' +
      escapeHtml(label) +
      "</span>" +
      '<span class="bubble-time">' +
      escapeHtml(timeStr) +
      "</span>" +
      "</div>" +
      '<div class="bubble-body">' +
      escapeHtml(bodyText) +
      "</div>" +
      "</div>"
    );
  }

  function renderConversation(t) {
    ensureFollowUps(t);
    const opened = t.createdAt;
    let html = "";

    html += bubbleHtml("user", "You", opened, t.message || "");

    const staffAt = opened ? offsetIso(opened, 90) : null;
    html += bubbleHtml("staff", "Support team", staffAt || opened, STAFF_PLACEHOLDER_RESPONSE);

    t.followUps.forEach(function (fu) {
      html += bubbleHtml("user", "You", fu.at, fu.body || "");
    });

    conversationThread.innerHTML = html;
    conversationThread.scrollTop = conversationThread.scrollHeight;
  }

  function showTicketDetailView(t) {
    t = ensureFollowUps(findTicket(t.id) || t);
    currentDetailTicketId = t.id;

    formPanel.hidden = true;
    historyPanel.hidden = true;
    ticketDetailPanel.hidden = false;
    ticketNotFound.hidden = true;
    ticketDetailContent.hidden = false;

    detailTicketId.textContent = t.id;
    detailSubject.textContent = t.subject || "—";
    detailCategory.textContent = formatCategory(t.category);

    if (t.createdAt) {
      detailCreated.dateTime = t.createdAt;
      detailCreated.textContent = formatWhen(t.createdAt);
    } else {
      detailCreated.removeAttribute("datetime");
      detailCreated.textContent = "—";
    }

    const name = t.contactName && String(t.contactName).trim();
    const email = t.contactEmail && String(t.contactEmail).trim();
    if (name || email) {
      detailContactLine.hidden = false;
      const parts = [];
      if (name) parts.push(name);
      if (email) parts.push(email);
      detailContactLine.textContent = "Contact: " + parts.join(" · ");
    } else {
      detailContactLine.hidden = true;
      detailContactLine.textContent = "";
    }

    const flashId = sessionStorage.getItem(FLASH_KEY);
    if (flashId && flashId === t.id) {
      sessionStorage.removeItem(FLASH_KEY);
      ticketDetailFlash.hidden = false;
      ticketDetailFlash.textContent =
        "Your inquiry was received. Your ticket ID is shown above—you can use it when following up with support.";
    } else {
      ticketDetailFlash.hidden = true;
      ticketDetailFlash.textContent = "";
    }

    followupMessage.value = "";
    renderConversation(t);

    document.title = "Ticket " + t.id + " — Transparency Portal";
    ticketDetailPanel.scrollIntoView({ behavior: "smooth", block: "start" });
  }

  function showNotFound(id) {
    currentDetailTicketId = null;
    formPanel.hidden = true;
    historyPanel.hidden = true;
    ticketDetailPanel.hidden = false;
    ticketNotFound.hidden = false;
    ticketDetailContent.hidden = true;
    ticketDetailFlash.hidden = true;
    notFoundId.textContent = id;
    document.title = "Ticket not found — Transparency Portal";
  }

  function route() {
    const id = parseTicketIdFromHash();
    if (!id) {
      showHomeChrome();
      return;
    }
    const t = findTicket(id);
    if (t) {
      showTicketDetailView(t);
    } else {
      showNotFound(id);
    }
  }

  function renderHistory() {
    const tickets = loadTickets();
    if (tickets.length === 0) {
      historyPanel.hidden = true;
      ticketHistory.innerHTML = "";
      return;
    }
    historyPanel.hidden = false;
    ticketHistory.innerHTML = "";
    tickets.forEach(function (t) {
      const li = document.createElement("li");
      const a = document.createElement("a");
      a.className = "ticket-card";
      a.href = "#ticket/" + t.id;
      a.setAttribute(
        "aria-label",
        "Open ticket " + t.id + (t.subject ? ": " + t.subject : "")
      );
      const time = t.createdAt ? new Date(t.createdAt).toLocaleString() : "";
      a.innerHTML =
        '<span class="ticket-id">' +
        escapeHtml(t.id) +
        "</span>" +
        '<span class="ticket-meta">' +
        escapeHtml(time) +
        " · " +
        escapeHtml(formatCategory(t.category)) +
        "</span>" +
        '<span class="ticket-subject">' +
        escapeHtml(t.subject || "") +
        "</span>" +
        '<span class="ticket-open-hint" aria-hidden="true">Open</span>';
      li.appendChild(a);
      ticketHistory.appendChild(li);
    });
  }

  function escapeHtml(s) {
    const div = document.createElement("div");
    div.textContent = s == null ? "" : String(s);
    return div.innerHTML;
  }

  function newId() {
    const existing = new Set(
      loadTickets().map(function (t) {
        return t.id;
      })
    );
    for (let i = 0; i < 100; i++) {
      const id = String(Math.floor(Math.random() * 100000000)).padStart(8, "0");
      if (!existing.has(id)) return id;
    }
    return String(Date.now() % 100000000).padStart(8, "0");
  }

  ticketForm.addEventListener("submit", function (e) {
    e.preventDefault();

    if (!ticketForm.checkValidity()) {
      ticketForm.reportValidity();
      return;
    }

    const record = {
      id: newId(),
      createdAt: new Date().toISOString(),
      category: category.value,
      subject: subject.value.trim(),
      message: message.value.trim(),
      contactName: contactName.value.trim() || null,
      contactEmail: contactEmail.value.trim() || null,
      followUps: [],
    };

    saveTicket(record);
    sessionStorage.setItem(FLASH_KEY, record.id);
    location.hash = "#ticket/" + record.id;
  });

  followupForm.addEventListener("submit", function (e) {
    e.preventDefault();
    if (!currentDetailTicketId) return;
    const text = followupMessage.value.trim();
    if (!text) {
      followupMessage.focus();
      return;
    }
    appendFollowUp(currentDetailTicketId, text);
    const updated = findTicket(currentDetailTicketId);
    if (updated) {
      renderConversation(updated);
      followupMessage.value = "";
      followupMessage.focus();
    }
  });

  function openNewInquiry() {
    goHome();
    ticketForm.reset();
    subject.focus();
  }

  btnTicketBack.addEventListener("click", function () {
    ticketForm.reset();
    goHome();
  });

  btnTicketAnother.addEventListener("click", function () {
    openNewInquiry();
  });

  linkNotFoundHome.addEventListener("click", function (e) {
    e.preventDefault();
    ticketForm.reset();
    goHome();
  });

  window.addEventListener("hashchange", function () {
    route();
  });

  route();

  var navLogout = document.getElementById("nav-logout");
  if (navLogout && typeof TPAuth !== "undefined") {
    navLogout.addEventListener("click", function () {
      TPAuth.logout();
    });
  }
})();
