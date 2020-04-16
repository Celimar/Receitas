using DevWebReceitas.Domain.Entities;
using DevWebReceitas.Domain.Filters;
using DevWebReceitas.Domain.Interfaces.Repositories;
using DevWebReceitas.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace DevWebReceitas.Domain.Services
{
    public class ReceitaDomainService : IReceitaDomainService
    {
        private readonly IReceitaRepository _receitaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public static IHostingEnvironment _environment;

        public ReceitaDomainService(IReceitaRepository receitaRepository
            , IHostingEnvironment environment
            , ICategoriaRepository categoriaRepository)
        {
            _receitaRepository = receitaRepository;
            _environment = environment;
            _categoriaRepository = categoriaRepository;
        }

        public void Create(Receita entity)
        {
            using (var trans = new TransactionScope())
            {
                SetCategoriaId(entity);
                GravaArquivo(entity);
                _receitaRepository.Create(entity);
                trans.Complete();
            }
        }

        public Receita FindById(int id)
        {
            var receita = _receitaRepository.FindById(id);
            return receita;
        }

        public Receita FindByCode(Guid code)
        {
            var receita = _receitaRepository.FindByCode(code);
            if (receita == null)
                throw new ArgumentException("Receita não encontrada");

            return receita;
        }

        public byte[] FindImageByCode(Guid codigo)
        {
            var receita = FindByCode(codigo);
            if (receita == null)
                throw new ArgumentException("Receita não encontrada");

            if (File.Exists(receita.CaminhoImagem))
                return File.ReadAllBytes(receita.CaminhoImagem);

            throw new ArgumentException("Arquivo não encontrado");
        }

        public IEnumerable<Receita> List(ReceitaFilter filter)
        {
            return _receitaRepository.List(filter);
        }

        public void Remove(int id)
        {
            _receitaRepository.Remove(id);
        }

        public void Remove(Guid codigo)
        {
            var receita = FindByCode(codigo);
            if (receita == null)
                throw new ArgumentException("Receita não encontrada");

            ExcluirArquivo(receita);
            _receitaRepository.Remove(codigo);
        }

        public void Update(Receita entity)
        {
            var receita = FindByCode(entity.Codigo);
            if (receita == null)
                throw new ArgumentException("Receita não encontrada");

            entity.Id = receita.Id;
            entity.DataUltimaAlteracao = DateTime.Now;
            entity.CaminhoImagem = receita.CaminhoImagem;

            using (var trans = new TransactionScope())
            {
                SetCategoriaId(entity);
                GravaArquivo(entity);
                _receitaRepository.Update(entity);
                trans.Complete();
            }
        }

        private void SetCategoriaId(Receita entity)
        {
            var categoria = _categoriaRepository.FindByCode(entity.Categoria.Codigo);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            entity.Categoria.Id = categoria.Id;
        }

        private static void GravaArquivo(Receita entity)
        {
            if (entity.HasImage)
            {
                var path = _environment.WebRootPath + "\\Upload\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (entity.CaminhoImagem != null)
                    ExcluirArquivo(entity);

                entity.NomeArquivo = Guid.NewGuid().ToString() + new FileInfo(entity.NomeArquivo).Extension;

                var fullPath = path + entity.NomeArquivo;
                using (Stream file = File.OpenWrite(fullPath))
                {
                    file.Write(entity.Imagem, 0, entity.Imagem.Length);
                    file.Flush();
                }

                entity.CaminhoImagem = fullPath;
            }
        }

        private static void ExcluirArquivo(Receita entity)
        {
            if (File.Exists(entity.CaminhoImagem))
            {
                File.Delete(entity.CaminhoImagem);
            }
            
        }

    }
}

