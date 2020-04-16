using System.Collections.Generic;
using System.Linq;
using DevWebReceitas.Application.Dtos.Receita;
using DevWebReceitas.Application.Mappers.Default;
using DevWebReceitas.Domain.Entities;

namespace DevWebReceitas.Application.Mappers.Receitas
{
    public class ReceitaEditDtoMapper : IReceitaEditDtoMapper
    {
        public ReceitaEditDto Map(Receita source)
        {
            return TypeConverter.ConvertTo<ReceitaEditDto>(source);
        }

        public IEnumerable<ReceitaEditDto> Map(IEnumerable<Receita> source)
        {
            return source.Select(x => Map(x));
        }
    }
}
