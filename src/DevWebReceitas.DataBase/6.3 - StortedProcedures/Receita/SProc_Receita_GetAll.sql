CREATE PROCEDURE [dbo].[SProc_Receita_GetAll]
As

 SELECT 
    R.[Id] 
    ,R.[Titulo] 
    ,R.Codigo
    ,R.[Descricao] 
    ,R.[ModoPreparo]
    ,R.[Ingredientes]
    ,R.[CaminhoImagem]
    ,R.[NomeArquivo]
    ,R.[CategoriaId]
    ,C.Codigo as  CategoriaCodigo
    ,C.[Nome] as CategoriaNome
	,R.[Ativo] 
	,R.[DataCadastro] 
    ,R.[DataUltimaAlteracao] 
 FROM dbo.tblReceitas R (nolock) 
 Join dbo.tblCategorias C (nolock) On C.Id = R.CategoriaId