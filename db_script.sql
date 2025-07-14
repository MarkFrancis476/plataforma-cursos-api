CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Instructors" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_Instructors" PRIMARY KEY ("Id")
);

CREATE TABLE "Courses" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "IsPublished" boolean NOT NULL,
    "InstructorId" uuid NOT NULL,
    CONSTRAINT "PK_Courses" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Courses_Instructors_InstructorId" FOREIGN KEY ("InstructorId") REFERENCES "Instructors" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Modules" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "CourseId" uuid NOT NULL,
    CONSTRAINT "PK_Modules" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Modules_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Lessons" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "ModuleId" uuid NOT NULL,
    CONSTRAINT "PK_Lessons" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Lessons_Modules_ModuleId" FOREIGN KEY ("ModuleId") REFERENCES "Modules" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Courses_InstructorId" ON "Courses" ("InstructorId");

CREATE INDEX "IX_Lessons_ModuleId" ON "Lessons" ("ModuleId");

CREATE INDEX "IX_Modules_CourseId" ON "Modules" ("CourseId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250714013804_InitialCreate', '8.0.6');

COMMIT;


-- =============================================
-- DATOS DE PRUEBA (SEED DATA)
-- =============================================

-- Borrar datos existentes para evitar conflictos (opcional, pero bueno para pruebas)
DELETE FROM "Lessons";
DELETE FROM "Modules";
DELETE FROM "Courses";
DELETE FROM "Instructors";

-- Instructores (10 registros)
INSERT INTO "Instructors" ("Id", "Name") VALUES
('a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6', 'Ada Lovelace'),
('b2c3d4e5-f6a7-b8c9-d0e1-f2a3b4c5d6e7', 'Grace Hopper'),
('c3d4e5f6-a7b8-c9d0-e1f2-a3b4c5d6e7f8', 'Alan Turing'),
('d4e5f6a7-b8c9-d0e1-f2a3-b4c5d6e7f8a9', 'John von Neumann'),
('e5f6a7b8-c9d0-e1f2-a3b4-c5d6e7f8a9b0', 'Margaret Hamilton'),
('f6a7b8c9-d0e1-f2a3-b4c5-d6e7f8a9b0c1', 'Tim Berners-Lee'),
('a7b8c9d0-e1f2-a3b4-c5d6-e7f8a9b0c1d2', 'Linus Torvalds'),
('b8c9d0e1-f2a3-b4c5-d6e7-f8a9b0c1d2e3', 'Dennis Ritchie'),
('c9d0e1f2-a3b4-c5d6-e7f8-a9b0c1d2e3f4', 'Bjarne Stroustrup'),
('d0e1f2a3-b4c5-d6e7-f8a9-b0c1d2e3f4a5', 'Guido van Rossum');

-- Cursos (100 registros)
-- Se generan 100 cursos asignados aleatoriamente a los 10 instructores
INSERT INTO "Courses" ("Id", "Title", "IsPublished", "InstructorId")
SELECT
    gen_random_uuid(),
    'Curso de ' || T.topic || ' ' || (floor(random() * 10) + 1),
    (random() > 0.5),
    (SELECT "Id" FROM "Instructors" ORDER BY random() LIMIT 1)
FROM (
    SELECT unnest(array['IA', 'Bases de Datos', 'Desarrollo Web', 'Ciberseguridad', 'Redes', 'Sistemas Operativos', 'Computación en la Nube', 'DevOps', 'Estructuras de Datos', 'Ingeniería de Software']) AS topic
) T, generate_series(1, 10);

-- Módulos (100 registros)
-- Se generan 100 módulos asignados aleatoriamente a los 100 cursos
INSERT INTO "Modules" ("Id", "Title", "CourseId")
SELECT
    gen_random_uuid(),
    'Módulo ' || (floor(random() * 5) + 1) || ': ' || T.topic,
    (SELECT "Id" FROM "Courses" ORDER BY random() LIMIT 1)
FROM (
    SELECT unnest(array['Introducción', 'Conceptos Avanzados', 'Proyecto Práctico', 'Herramientas', 'Examen Final']) AS topic
) T, generate_series(1, 20);

-- Lecciones (100 registros)
-- Se generan 100 lecciones asignadas aleatoriamente a los 100 módulos
INSERT INTO "Lessons" ("Id", "Title", "ModuleId")
SELECT
    gen_random_uuid(),
    'Lección ' || (floor(random() * 100) + 1) || ': ' || T.topic,
    (SELECT "Id" FROM "Modules" ORDER BY random() LIMIT 1)
FROM (
    SELECT unnest(array['Configuración del Entorno', 'Primeros Pasos', 'Sintaxis Básica', 'Ejercicios Prácticos', 'Resumen del Módulo']) AS topic
) T, generate_series(1, 20);