using DevWebReceitas.Application.Dtos.Receita;
using DevWebReceitas.Domain.Filters;
using System;
using System.Collections.Generic;

namespace DevWebReceitas.Application.Interfaces
{
    public interface IReceitaService
    {
        Guid Create(ReceitaInsertDto entity);
        void Remove(Guid codigo);
        ReceitaDto FindByCode(Guid codigo);
        ReceitaEditDto FindByCodeEditDto(Guid codigo);
        IEnumerable<ReceitaDto> List(ReceitaFilter filter);
        void Update(ReceitaEditDto entity);
        void Update(ReceitaDto entity);

        byte[] FindImageByCode(Guid codigo);
    }
}
