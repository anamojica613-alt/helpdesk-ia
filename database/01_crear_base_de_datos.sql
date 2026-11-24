-- HelpDesk IA - Script de creación de la base de datos (SQL Server)
-- Ejecutar completo en SQL Server Management Studio.

CREATE DATABASE HelpDeskIA;
GO
USE HelpDeskIA;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL UNIQUE,
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Solicitante','Agente','Admin'))
);

CREATE TABLE Categorias (
    Id INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(60) NOT NULL UNIQUE
);

CREATE TABLE Tickets (
    Id INT IDENTITY PRIMARY KEY,
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    CategoriaId INT NULL REFERENCES Categorias(Id),
    Prioridad NVARCHAR(10) NOT NULL DEFAULT 'Media'
        CHECK (Prioridad IN ('Baja','Media','Alta')),
    Estado NVARCHAR(15) NOT NULL DEFAULT 'Abierto'
        CHECK (Estado IN ('Abierto','EnProceso','Cerrado')),
    SolicitanteId INT NOT NULL REFERENCES Usuarios(Id),
    AsignadoAId INT NULL REFERENCES Usuarios(Id),
    SugerenciaIA NVARCHAR(MAX) NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    FechaCierre DATETIME2 NULL
);

CREATE TABLE Comentarios (
    Id INT IDENTITY PRIMARY KEY,
    TicketId INT NOT NULL REFERENCES Tickets(Id),
    UsuarioId INT NOT NULL REFERENCES Usuarios(Id),
    Texto NVARCHAR(MAX) NOT NULL,
    Fecha DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Datos de ejemplo
INSERT INTO Categorias (Nombre) VALUES
('Hardware'),('Software'),('Redes'),('Accesos y contraseñas'),('Correo');

INSERT INTO Usuarios (Nombre, Correo, Rol) VALUES
('Agente de ejemplo', 'agente@ejemplo.com', 'Agente'),
('Solicitante de ejemplo', 'solicitante@ejemplo.com', 'Solicitante');
