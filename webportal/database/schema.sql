-- Transparency portal: three tables (questions, preset answers, user inquiries).
--
-- Easiest setup: create an empty database, then from TransparencyPortal.Api run:
--   dotnet ef database update
-- That applies the EF migration (snake_case columns via naming conventions) and the app seeds two topics on startup.
--
-- Manual SQL is optional; the schema is owned by EF migrations in Data/Migrations/.

CREATE DATABASE IF NOT EXISTS Transparency_Portal;
USE Transparency_Portal;
