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

-- Criação da tabela de jogos
IF OBJECT_ID('Jogos', 'U') IS NULL
BEGIN
    CREATE TABLE Jogos (
        Id INT IDENTITY PRIMARY KEY,
        DataInicio DATETIME NOT NULL DEFAULT GETDATE(),
        DataFim DATETIME NULL
    );
END
GO

-- Movimentos de cada jogo
IF OBJECT_ID('Movimentos', 'U') IS NULL
BEGIN
    CREATE TABLE Movimentos (
        Id INT IDENTITY PRIMARY KEY,
        JogoId INT NOT NULL,
        Jogador NVARCHAR(20) NOT NULL, -- 'Humano' ou 'IA'
        PalitosRemovidos INT NOT NULL,
        DataMovimento DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Movimentos_Jogos FOREIGN KEY (JogoId) REFERENCES Jogos(Id)
    );
END
GO
