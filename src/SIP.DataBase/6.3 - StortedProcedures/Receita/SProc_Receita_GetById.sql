﻿CREATE PROCEDURE [dbo].[SProc_Receita_GetById]  
(  
 @Id int  
)  
As  
  
 SELECT 
    R.[Id] 
    ,R.[Titulo] 
    ,R.Codigo
    ,R.[Descricao] 
    ,R.[ModoPreparo] 
    ,R.[CategoriaId]
    ,C.[Nome] as CategoriaNome
	,R.[Ativo] 
	,R.[DataCadastro] 
    ,R.[DataUltimaAlteracao] 
 FROM dbo.tblReceitas R (nolock) 
 Join dbo.tblCategorias C (nolock) On C.Id = R.CategoriaId
 Where R.Id = @Id  
 