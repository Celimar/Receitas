﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevWebReceitas.Application.Dtos;
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
        private readonly IReceitaMapper _receitaMapper;
        private readonly IReceitaDtoMapper _receitaDtoMapper;


        public ReceitaService(IReceitaDomainService service,
                                IReceitaMapper receitaMapper,
                                IReceitaDtoMapper receitaDtoMapper)
        {
            _service = service;
            _receitaMapper = receitaMapper;
            _receitaDtoMapper = receitaDtoMapper;
        }

        public Guid Create(ReceitaInsertDto dto)
        {
            var id = Guid.NewGuid();
            var objPersistencia = new Receita
            {
                Id = id,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                ModoPreparo = dto.ModoPreparo,
                Itens = dto.Itens.Select(x => new Item
                {
                    Id = Guid.NewGuid(),
                    ReceitaId = id,
                    Quantidade = x.Quantidade,
                    Obs = x.Obs,
                    Ingrediente = new Ingrediente { Id = x.IngredienteId }
                })
            }; 

            _service.Create(objPersistencia);
            return objPersistencia.Id;
        }

        public IEnumerable<ReceitaDto> List(ReceitaFilter filter)
        {
            var list = _service.List(filter);
            return _receitaDtoMapper.Map(list);
        }

        public void Remove(Guid id)
        {
            _service.Remove(id);
        }

        public ReceitaDto FindById(Guid id)
        {
            var receita = _service.FindById(id);
            if (receita == null)
                throw new ArgumentException("Receita não encontrada");

            return _receitaDtoMapper.Map(receita);
        }

        public void Update(ReceitaDto entity)
        {
            _service.Update(TypeConverter.ConvertTo<Receita>(entity));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}



