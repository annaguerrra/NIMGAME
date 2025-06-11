IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'NimGame')
BEGIN
    CREATE DATABASE NimGame;
END
GO

USE NimGame;
GO

-- Tabela de usuários
IF OBJECT_ID('usuarios', 'U') IS NULL
BEGIN
    CREATE TABLE usuarios (
        id INT IDENTITY(1,1) PRIMARY KEY,
        email VARCHAR(255) UNIQUE NOT NULL,
        nome_usuario VARCHAR(50) UNIQUE NOT NULL,
        senha_hash VARCHAR(255) NOT NULL,
        data_cadastro DATETIME DEFAULT GETDATE()
    );
END
GO