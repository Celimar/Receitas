using System;
using System.Collections.Generic;
using DevWebReceitas.Application.Dtos.Receita;
using DevWebReceitas.Application.Extensions;
using DevWebReceitas.Application.Interfaces;
using DevWebReceitas.Application.Mappers.Default;
using DevWebReceitas.Application.Mappers.Receitas;
using DevWebReceitas.Domain.Entities;
using DevWebReceitas.Domain.Filters;
using DevWebReceitas.Domain.Services.Interfaces;

namespace DevWebReceitas.Application.Services
{
    public class ReceitaService : IReceitaService, IDisposable
    {
        private readonly IReceitaDomainService _service;
        private readonly IReceitaDtoMapper _receitaDtoMapper;
        private readonly IReceitaEditDtoMapper _receitaEditDtoMapper;


        public ReceitaService(IReceitaDomainService service,
                                IReceitaDtoMapper receitaDtoMapper,
                                IReceitaEditDtoMapper receitaEditDtoMapper)
        {
            _service = service;
            _receitaDtoMapper = receitaDtoMapper;
            _receitaEditDtoMapper = receitaEditDtoMapper;
        }

        public Guid Create(ReceitaInsertDto dto)
        {
            var codigo = Guid.NewGuid();
            var objPersistencia = new Receita
            {
                Codigo = codigo,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                ModoPreparo = dto.ModoPreparo,
                Ingredientes = dto.Ingredientes,
                Imagem = dto.Imagem != null ? dto.Imagem.ConvertToBytes(): null,
                NomeArquivo = dto.Imagem != null ? dto.Imagem.FileName : null,
                Categoria = new Categoria { Codigo = dto.CodigoCategoria }
            };

            _service.Create(objPersistencia);
            return objPersistencia.Codigo;
        }

        public IEnumerable<ReceitaDto> List(ReceitaFilter filter)
        {
            var list = _service.List(filter);
            return _receitaDtoMapper.Map(list);
        }

        public void Remove(Guid codigo)
        {
            _service.Remove(codigo);
        }

        public ReceitaDto FindByCode(Guid codigo)
        {
            var receita = _service.FindByCode(codigo);
            return _receitaDtoMapper.Map(receita);
        }
        public ReceitaEditDto FindByCodeEditDto(Guid codigo)
        {
            var receita = _service.FindByCode(codigo);
            return _receitaEditDtoMapper.Map(receita);
        }

        public byte[] FindImageByCode(Guid codigo)
        {
            return _service.FindImageByCode(codigo);
        }

        public void Update(ReceitaEditDto dto)
        {
            var objPersistencia = new Receita
            {
                Codigo = dto.Codigo,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                ModoPreparo = dto.ModoPreparo,
                Ingredientes = dto.Ingredientes,
                Imagem = dto.Imagem != null ? dto.Imagem.ConvertToBytes() : null,
                NomeArquivo = dto.Imagem != null ? dto.Imagem.FileName : null,
                Categoria = new Categoria { Codigo = dto.CodigoCategoria }
            };

            _service.Update(objPersistencia);
        }
        public void Update(ReceitaDto dto)
        {
            var objPersistencia = new Receita
            {
                Codigo = dto.Codigo,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                ModoPreparo = dto.ModoPreparo,
                Ingredientes = dto.Ingredientes,
                Imagem = dto.Imagem != null ? dto.Imagem.ConvertToBytes() : null,
                NomeArquivo = dto.Imagem != null ? dto.Imagem.FileName : null,
                Categoria = new Categoria { Codigo = dto.Categoria.Codigo }
            };

            _service.Update(objPersistencia);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}



