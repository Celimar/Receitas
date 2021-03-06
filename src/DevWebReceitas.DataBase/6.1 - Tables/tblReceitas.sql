﻿CREATE TABLE [dbo].[tblReceitas] (
    [Id] int identity(1,1) NOT NULL,
    [Codigo] uniqueidentifier NOT NULL,
    [Titulo] varchar(150) NOT NULL DEFAULT '',
    [Descricao] varchar(256) NULL DEFAULT '',
    [Ingredientes] varchar(MAX) NULL DEFAULT '',
    [ModoPreparo] varchar(MAX) NULL DEFAULT '',
    [CaminhoImagem] varchar(256) NULL DEFAULT '',
    [NomeArquivo] varchar(100) NULL DEFAULT '',
    [CategoriaId] smallint NULL,
	[Ativo] bit NOT NULL,
	[DataCadastro] datetime NOT NULL,
    [DataUltimaAlteracao] DATETIME NULL, 
    CONSTRAINT [PK_Receita] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_tblReceitas_tblCategorias] FOREIGN KEY ([CategoriaId]) REFERENCES [tblCategorias]([Id])
);
GO
CREATE INDEX [NC_tblReceitas_CategoriaId] ON [dbo].[tblReceitas] ([CategoriaId])

GO
CREATE INDEX [NC_tblReceitas_Codigo] ON [dbo].[tblReceitas] ([Codigo])
