IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Autores] (
    [CodAutor] int NOT NULL IDENTITY,
    [NomeAutor] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY ([CodAutor])
);
GO

CREATE TABLE [Editoras] (
    [CodEditora] int NOT NULL IDENTITY,
    [NomeEditora] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Editoras] PRIMARY KEY ([CodEditora])
);
GO

CREATE TABLE [Locais] (
    [CodLocal] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Locais] PRIMARY KEY ([CodLocal])
);
GO

CREATE TABLE [Secoes] (
    [CodSecao] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Secoes] PRIMARY KEY ([CodSecao])
);
GO

CREATE TABLE [Itens] (
    [CodItem] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [TipoItem] nvarchar(max) NOT NULL,
    [NumExemplar] nvarchar(max) NOT NULL,
    [Volume] nvarchar(max) NOT NULL,
    [Edicao] nvarchar(max) NOT NULL,
    [Idioma] nvarchar(max) NOT NULL,
    [CodAutor] int NOT NULL,
    [CodEditora] int NOT NULL,
    [CodLocal] int NOT NULL,
    [CodSecao] int NOT NULL,
    CONSTRAINT [PK_Itens] PRIMARY KEY ([CodItem]),
    CONSTRAINT [FK_Itens_Autores_CodAutor] FOREIGN KEY ([CodAutor]) REFERENCES [Autores] ([CodAutor]) ON DELETE CASCADE,
    CONSTRAINT [FK_Itens_Editoras_CodEditora] FOREIGN KEY ([CodEditora]) REFERENCES [Editoras] ([CodEditora]) ON DELETE CASCADE,
    CONSTRAINT [FK_Itens_Locais_CodLocal] FOREIGN KEY ([CodLocal]) REFERENCES [Locais] ([CodLocal]) ON DELETE CASCADE,
    CONSTRAINT [FK_Itens_Secoes_CodSecao] FOREIGN KEY ([CodSecao]) REFERENCES [Secoes] ([CodSecao]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Itens_CodAutor] ON [Itens] ([CodAutor]);
GO

CREATE UNIQUE INDEX [IX_Itens_CodEditora] ON [Itens] ([CodEditora]);
GO

CREATE UNIQUE INDEX [IX_Itens_CodLocal] ON [Itens] ([CodLocal]);
GO

CREATE UNIQUE INDEX [IX_Itens_CodSecao] ON [Itens] ([CodSecao]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221130143129_InitialMigration', N'6.0.0');
GO

COMMIT;
GO

