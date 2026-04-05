---
stepsCompleted:
  - step-01-init
  - step-02-discovery
  - step-02b-vision
  - step-02c-executive-summary
  - step-03-success
  - step-04-journeys
  - step-05-domain
  - step-06-innovation
  - step-07-project-type
  - step-08-scoping
  - step-09-functional
  - step-10-nonfunctional
  - step-11-polish
  - step-12-complete
workflowNotes:
  portalStatus: "No existing deployed portal; greenfield product."
  schemaSql: "Rough draft DDL for v1 only—not a legacy system to extend."
  innovationSection: skipped
  releaseType: "User-testing prototype (dual portals); see Project Scoping section."
inputDocuments:
  - "_bmad-output/planning-artifacts/prd-input-background.md"
  - "webportal/database/schema.sql"
workflowType: "prd"
documentCounts:
  briefCount: 0
  researchCount: 0
  brainstormingCount: 0
  projectDocsCount: 2
classification:
  projectType: web_app
  domain: general
  complexity: medium
  projectContext: greenfield
---

# Product Requirements Document - mis430-final

**Author:** Hoang
**Date:** 2026-04-04

## Executive Summary

The organization operates a large, U.S.–focused consumer digital platform where automated and AI-supported systems influence **who gets access to services or opportunities**, **how work is prioritized or queued**, and **what content, options, or recommendations people see**. These outcomes are not life-or-death, but they materially shape experience, opportunity, and perceptions of fairness. Stakeholders—users, regulators, and the public—increasingly expect **transparency** into how automation and AI affect people, alongside defensible **fairness**, **explainability**, and **accountability**.

This product is a **greenfield, public-facing web support portal** whose primary job is **easy access**: a simple entry point where users can understand transparency commitments without hunting through static policy pages alone. **Success for users** is practical clarity and a reduction in the sense that outcomes are arbitrary—**decisions should not feel “unfair”** because the system is opaque or unreachable. **Success for the organization** is sustained **trust** and defensible communication under **competitive pressure** and **emerging AI and automation scrutiny**.

### What Makes This Special

The portal is **more than a static FAQ**. It is an **interactive transparency experience**: users start from concise, vetted answers and can **ask deeper, structured questions** about the transparency policy and how automated and AI-supported decisions are framed—going beyond the first layer of help without replacing individualized review where the organization chooses to offer it. Differentiation is **depth without clutter**—interactive exploration tied to published transparency intent, not a generic marketing site or a black-box chatbot disconnected from policy.

## Project Classification

| Dimension | Classification |
|-----------|------------------|
| **Project type** | Web application (browser-based, public-facing support portal) |
| **Domain** | General consumer digital services; cross-cutting themes of trust, transparency, and AI/automation accountability |
| **Complexity** | Medium—meaningful fairness, explainability, and stakeholder expectations; not safety-critical |
| **Project context** | **Greenfield** product. `schema.sql` reflects a **v1 draft** data model, not an existing deployed portal. |

## Success Criteria

### User Success

- **Access:** A typical user can **reach the portal’s main transparency experience** from a predictable entry point (e.g. linked from help, account, or public site) **without** specialist knowledge or internal URLs.
- **Clarity:** Users can **complete a primary transparency path** (e.g. understand how automation/AI affects them at a high level) using **vetted default content** in one session.
- **Depth:** Users who need more can **move from starter FAQs into structured, in-depth questions** (interactive FAQ) and receive **consistent, policy-aligned** responses—not ad hoc or model-fabricated claims outside approved content.
- **Fairness perception:** Qualitative target—users report that decisions feel **less arbitrary** because expectations and limits are **explained in plain language** (measured via lightweight feedback where the product allows it).

### Business Success

- **Trust and scrutiny:** The organization can **point stakeholders** (users, press, regulators) to a **single, credible** transparency surface that matches published commitments.
- **Operational load:** **Deflection** of generic transparency questions toward self-serve content where appropriate; **clear routing** where human review is required (scope defined in implementation).
- **Time horizon:** **Near term (e.g. 3–6 months):** launch MVP, stable usage baseline, no major trust incidents tied to “no transparency surface.” **Longer term:** sustained use and positive or neutral sentiment in feedback channels tied to transparency.

### Technical Success

- **Reliability:** Portal meets agreed **availability** targets for a public support property (exact SLO to be set with engineering).
- **Integrity:** **Approved content** (e.g. preset responses, default questions) is **versioned and traceable**; users are not served undeclared generative answers as fact when policy is for fixed or curated content.
- **Security & privacy:** **No unnecessary PII** in public flows; inquiries and follow-ups handled per organizational policy.

### Measurable Outcomes

- **Usage:** Sessions, completion of primary flows, depth usage (e.g. follow-on questions vs. bounce).
- **Quality:** Content coverage gaps identified and backlog; incident rate for “wrong or misleading transparency answer.”
- **Sentiment:** Optional **in-portal** or **post-task** micro-surveys where permitted.

## Product Scope

**Scope note:** The **current delivery** is a **user-testing prototype** (dual portals: user + staff; see [Project Scoping & Phased Development](#project-scoping--phased-development)). The phases below describe the **longer-term** trajectory; where they conflict with the prototype section, **the prototype section wins** for this release.

### MVP - Minimum Viable Product

- Public **web** transparency/support portal with **easy navigation** and **mobile-friendly** layout.
- **Curated default questions** and **preset, approved answers** aligned to transparency policy.
- **Interactive FAQ path**: users can **select or submit structured follow-ons** within bounds of v1 (per draft schema: e.g. inquiries linked to default questions / presets).
- **Clear disclosure** when answers are **fixed/preset** vs. **individual review** (if offered in v1).
- **Basic** feedback or “was this helpful?” where feasible.

### Growth Features (Post-MVP)

- Richer **personalization** of paths (still policy-bound).
- **Analytics** for content gaps and regulatory-style reporting exports (if required).
- **Accessibility** and localization **beyond** v1 baseline if not fully in MVP.

### Vision (Future)

- Deeper integration with **internal transparency operations**, **continuously updated** content pipelines, and **expanded** question classes—still **governed** so trust and accountability remain central.

## User Journeys

### 1. Jordan — primary user, success path (clarity in one visit)

**Opening:** Jordan notices something odd about what they see on the platform and feels the system is “hiding the rules.” They open the **Transparency & support** link from the main app help menu.

**Rising action:** They land on a **short, plain-language overview** of how automation and AI shape access, ordering, and recommendations. They open a **default question** that matches their worry (e.g. prioritization or content ordering). They read a **preset, vetted answer** that explains boundaries without jargon.

**Climax:** They use one **structured follow-on** (interactive FAQ) to go one level deeper; the response stays **aligned with published policy** and clearly states what is **fixed guidance** vs. what needs a **human** for their specific case.

**Resolution:** Jordan leaves with a **clear mental model** and lower frustration—“I get what the system is doing at a high level.” They optionally tap **“Was this helpful?”** and move on without contacting support.

**Emotional arc:** Suspicion → curiosity → relief (or at least **reduced** helplessness).

### 2. Sam — primary user, edge case (needs an individualized path)

**Opening:** Sam’s situation doesn’t match the FAQ; they believe they were treated **unfairly** in a way the generic answers don’t address.

**Rising action:** They still start from the same portal for **legitimacy and consistency**, read the default content, then choose a path for **custom or individualized inquiry** (if offered in v1). They provide **structured detail** within the product’s limits. The UI sets expectations: **timeline**, **what staff can/can’t decide**, and **privacy** notices.

**Climax:** The system records an **inquiry** (per draft schema) without promising an outcome; Sam sees a **confirmation** and knows **first substantive response** will come from staff, not from an unapproved bot.

**Resolution:** Even if the answer later disappoints, Sam’s **trust in the process** is higher because the route was **transparent and respectful**—not a dead end or opaque form.

**Emotional arc:** Anger/anxiety → cautious engagement → **stabilized** trust (outcome may still be negative).

### 3. Riley — content / policy operations (publishes and maintains truth)

**Opening:** Riley owns **transparency copy** and must update answers after a **policy or model** change without breaking trust.

**Rising action:** Riley uses an **internal or admin** workflow (exact tool TBD) to **edit default questions**, **publish new preset response versions**, and **deprecate** old language. Changes are **versioned** and tied to **release notes** or internal approvals.

**Climax:** A planned communication goes out; the **public portal** shows **consistent** wording with what legal/comms approved—no drift between channels.

**Resolution:** Regulators and press citations stay aligned with **one source of truth**; support tickets drop for “outdated FAQ” confusion.

### 4. Casey — support / trust & safety liaison (investigation handoff)

**Opening:** Casey gets escalations: “The portal said X but my account did Y.”

**Rising action:** Casey verifies which **preset version** the user likely saw, whether an **inquiry** exists, and **first response** timestamps. They use internal tools to **link** the case to the right team.

**Climax:** Casey responds with **language consistent with vetted content** and avoids **ad-hoc** explanations that contradict policy.

**Resolution:** Fewer **contradictory** messages to users; faster closure with **audit-friendly** handling.

### Journey Requirements Summary

| Area | Capabilities suggested by journeys |
|------|-----------------------------------|
| **Discovery & entry** | Obvious entry from product/help; mobile-friendly reading; minimal cognitive load on landing. |
| **Interactive FAQ** | Default questions + preset responses; structured follow-ons; clear labels for **preset vs. individualized** handling. |
| **Inquiries** | Capture structured custom questions; expectations on review; timestamps / status if in scope (aligns with draft `inquiries` model). |
| **Trust & integrity** | Versioned content; no undeclared generative answers as fact when policy requires curated content. |
| **Operations** | Admin/content pipeline for Q&A lifecycle; support workflows to trace what users saw. |
| **Measurement** | Helpfulness feedback; basic analytics on paths and depth usage. |

## Domain-Specific Requirements

### Compliance & regulatory

- **Emerging AI and automation policy:** Product copy and behavior should assume **changing** expectations (U.S. federal and state activity, sector guidance). The portal should support **accurate, reviewable** statements—not dynamic claims that bypass policy review.
- **Consumer-facing fairness and deception:** Avoid **misleading** implications that individualized outcomes were decided in the portal when they were not; align with **truth-in-advertising** and plain-language norms for public communications (exact legal sign-off is out of scope for the PRD but **legal/comms review** is assumed for published content).
- **Records and accountability:** Where inquiries or follow-ups exist, support **reasonable** retention and traceability for **internal investigations** and **regulatory** requests (specifics TBD with legal).

### Technical constraints

- **Privacy:** Minimize **PII** in self-serve flows; secure handling for any **account-linked** inquiry path; clear **retention** and **access** boundaries.
- **Security:** Standard web hardening; **access control** for admin/content tools; **auditability** of content changes (who published what, when).
- **Integrity of answers:** When “preset” or **human-reviewed** content is required, **technical controls** should reduce risk of unapproved generative text presented as official policy.

### Integration requirements

- **Corporate site / app:** Deep links from **help**, **account**, or **public** surfaces; consistent navigation back to the main product.
- **Internal ops:** Workflow to **content management**, **support**, and optionally **case** systems for individualized inquiries (depth of integration TBD for MVP).

### Risk mitigations

| Risk | Mitigation |
|------|------------|
| **Trust incident** from wrong or drifting copy | Versioned Q&A; approval workflow; monitoring and rollback |
| **Regulatory or press** quoting outdated text | Single source of truth; dated or versioned disclosures where needed |
| **User harm** from false certainty | Clear boundaries on what the portal can decide vs. staff review |
| **Scope creep** into “AI explains my ban” | Keep v1 to **policy-aligned**, **governed** content paths |

## Web Application Specific Requirements

### Project-type overview

Public **browser-based** transparency portal: server-rendered or hybrid entry for **SEO** and fast first paint, with **client-side** behavior for interactive FAQ depth and inquiry flows. **No** native app in v1 scope per project-type `skip_sections`.

### Technical architecture considerations

- **Hosting:** Standard HTTPS web stack; separate **API** layer if front end and back end are split (aligns with .NET + MySQL direction elsewhere in the project).
- **Routing:** Stable, shareable URLs for major transparency topics (supports SEO and citations).
- **Client state:** Sufficient UX state for multi-step FAQ without full page reloads where it improves clarity.

### Browser matrix

| Tier | Browsers / engines (indicative) |
|------|----------------------------------|
| **A (full)** | Latest two major versions of **Chrome**, **Edge**, **Firefox**, **Safari** (desktop); current **Safari** and **Chrome** on **iOS** and **Android**. |
| **B (best effort)** | Older evergreen within security support window—define exact floor with engineering. |

### Responsive design

- **Mobile-first** reading for long policy-style content; touch-friendly controls for FAQ expansion and inquiry forms.
- Breakpoints and typography tuned so **plain-language** content remains readable without zooming.

### Performance targets

- **Core path:** landing → first default answer usable on mid-tier mobile on common networks (specific **LCP/INP** budgets TBD with engineering).
- **Asset discipline:** lean JS for v1; avoid heavy client bundles that hurt trust on slow connections.

### SEO strategy

- Indexable **public** pages where appropriate; **meta** descriptions and titles aligned with comms/legal wording.
- Avoid indexing **authenticated** or **personalized** inquiry views if they exist; use **robots** rules as needed.

### Accessibility level

- Target **WCAG 2.1 Level AA** for primary flows: structure, contrast, keyboard use, form labels, error identification.
- **Plain language** pairs with accessibility for cognitive load (not a WCAG substitute).

### Implementation considerations

- **Security headers**, **CSP** (as appropriate), and safe handling of any **PII** in inquiry flows.
- **Analytics** and **feedback** widgets must not block primary content or violate privacy commitments.
- **Content updates** deployable without full app redeploy where possible (CMS or config-driven Q&A).

## Project Scoping & Phased Development

### Release focus (this delivery)

**This release is a prototype for user testing**, not a full production launch. Scope prioritizes **learnability** (can users understand transparency content and flows?) and **technical feasibility** of the core mechanics over scale, polish, or full operational integration.

### Product surface area

- **Two web portals:** one **end-user** portal and one **staff** portal (separate entry points/access assumptions).
- **No requirement** for **back-and-forth conversation** (e.g. synchronous chat threading between user and staff) in this release.

### User portal — core behavior

- Users can obtain information by choosing from **several pre-set (curated) questions**.
- Users can **elaborate** on their situation or question where the flow provides that option.
- For selected preset paths, users receive **pre-generated responses** tied to those questions (not ad hoc generated policy).
- Users can **ask additional questions that are not preset**, still **on the same topic**, beyond the initial preset set.
- Flows are **request/response and structured steps**, not a mandatory conversational loop.

### Staff portal — core behavior

- Staff can **see users’ tickets** (or equivalent records).
- Staff can **see the questions** associated with those tickets (including preset selections, elaborations, and non-preset follow-on questions as implemented).

### MVP / prototype feature set (Phase 1)

**In scope**

- Dual portals (user + staff) at a level sufficient for **user testing**.
- Preset question catalog, pre-generated answers, optional elaboration, and non-preset follow-on questions **within topic**, recorded on a **ticket**.
- Staff visibility into tickets and question text.

**Explicitly out of scope for this prototype (unless later pulled in)**

- Real-time chat or **ongoing dialogue** between user and staff.
- Full production **SLA**, **localization**, or **deep** integrations beyond what testing requires.

### Post-prototype (later phases)

- Hardening for production, richer **ops** tooling, analytics, and integrations—per earlier roadmap themes once the prototype validates flows.

### Risk mitigation strategy

**Technical risks:** Keep **ticket and question model** simple; align with draft DDL (`inquiries`, `default_questions`, `preset_responses`) and avoid scope creep into conversational AI.

**User-testing risks:** Prototype clearly labeled internally; **content** approved for **external** user tests only when legal/comms ready.

**Resource risks:** If time-constrained, reduce **cosmetic** polish before cutting **dual-portal** or **ticket visibility**—those are central to the test.

## Functional Requirements

### User portal — access and transparency content

- **FR1:** An end user can open and use the **user-facing** transparency portal without staff credentials.
- **FR2:** An end user can see **available pre-set questions** (or equivalent grouped choices) offered by the product.
- **FR3:** An end user can **select** a pre-set question to request information.
- **FR4:** The system can present a **pre-generated response** tied to the selected pre-set question (from approved content, not improvised policy text).
- **FR5:** An end user can **optionally elaborate** (free text or structured elaboration) when the flow provides that step.
- **FR6:** An end user can submit **additional questions that are not pre-set**, still **on the current topic**, when the flow provides that step.
- **FR7:** The core user flow does **not** depend on **real-time back-and-forth** with staff (no required conversational loop with staff to get value from preset/pre-generated content).

### Tickets and recorded activity

- **FR8:** The system creates and persists a **ticket** (or equivalent record) for an end user’s session or submission path.
- **FR9:** The system associates with a ticket: **selected pre-set question(s)**, **elaboration** (if any), **pre-generated response(s)** shown, and **non-preset follow-on question(s)** (if any), as implemented.
- **FR10:** The system can represent **topic or path** context (e.g. which transparency area the user is in) when the product defines multiple paths.

### Staff portal — visibility

- **FR11:** A **staff** user can sign in to a **staff-only** portal (or staff-only area) that is separate from the end-user experience.
- **FR12:** A staff user can view a **list of tickets** from user activity.
- **FR13:** A staff user can open a ticket and view the **questions and elaborations** recorded for that user.
- **FR14:** A staff user can see **which interactions** came from **pre-set** flows versus **non-preset** follow-on questions when the system stores that distinction.
- **FR15:** Unauthenticated users **cannot** access staff ticket views.

### Prototype and testing

- **FR16:** The product can be run as a **prototype** suitable for **user testing** (tasks, observation, and feedback capture as defined for the study).

### Out of scope for this release (capability exclusions)

- **FR17:** **Synchronous** or **threaded chat** between end user and staff is **not** required for this release (may exist later; not part of the capability contract for this prototype).

## Non-Functional Requirements

### Performance

- **NFR-P1:** Primary user paths (open portal → view presets → receive pre-generated response → optional elaboration / follow-on) complete without **unacceptable** wait for **moderated user tests**; exact thresholds are set with engineering (prototype may use relaxed targets vs. production).
- **NFR-P2:** Staff list and ticket detail views load in time that does not block **test observation** (qualitative bar for prototype).

### Security

- **NFR-S1:** **Staff-only** content (ticket lists, user questions) is available only after **successful staff authentication** and is not exposed through the unauthenticated user portal.
- **NFR-S2:** **Transport security:** traffic uses **HTTPS** in test and production-like environments.
- **NFR-S3:** **Data at rest** for tickets and question text uses protections appropriate to the **sensitivity** agreed with legal/privacy (minimum: access limited to authorized staff and hardened defaults for the stack).
- **NFR-S4:** The system avoids collecting **unnecessary PII** in user flows; any PII collected follows **retention and access** rules defined for the prototype (TBD with stakeholders).

### Scalability

- **NFR-SC1:** For this **prototype**, the system supports **concurrent test sessions** and small **pilot** volume; **large-scale** load targets are **post-prototype** unless testing explicitly requires load simulation.

### Accessibility

- **NFR-A1:** User-facing portal primary flows are usable with **keyboard** and **screen-reader** basics suitable for **inclusive user testing**; target alignment with **WCAG 2.1 Level AA** for those flows where feasible in prototype (full audit may be post-prototype).
- **NFR-A2:** Legible **default typography** and **contrast** on user portal pages carrying transparency content.

### Integration

- **NFR-I1:** No **mandatory** integration with external case or CRM systems for the **prototype**; exports or handoff formats are **optional** and defined only if needed for the test.
- **NFR-I2:** Deep links from the main product (if used in tests) use **stable URLs** where the prototype defines them.

### Reliability

- **NFR-R1:** For scheduled **user tests**, the environment is **available** for the test window (best-effort for prototype; no formal SLA required in this PRD for the prototype phase).
