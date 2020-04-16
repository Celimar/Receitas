using System;
using System.IO;
using DevWebReceitas.Application.Dtos.Receita;
using DevWebReceitas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace DevWebReceitas.UI.Controllers
{
    public class ReceitaController : Controller
    {
        private readonly IReceitaService _service;
        private readonly ICategoriaService _categService;
        
        public ReceitaController(IReceitaService service, ICategoriaService categService)
        {
            _service = service;
            _categService = categService;
        }

        // GET: Receita
        public ActionResult Index()
        {
            var receitas = _service.List(new Domain.Filters.ReceitaFilter());
            return View(receitas);
        }

        // GET: Receita/Details/XXXXXXX-XXXXXXX-XXXXXXX-XXXXXXXX
        public ActionResult Details(Guid codigo)
        {
            var receita = _service.FindByCode(codigo);
            try
            {
                ViewBag.Imagem = _service.FindImageByCode(codigo);
            }
            catch (Exception)
            {
            }
            return View(receita);
        }

        // GET: Receita/Create
        public ActionResult Create()
        {
            var categorias =_categService.List(new Domain.Filters.CategoriaFilter());
            if (categorias.ToList().Count == 0)
            {
                ModelState.AddModelError("Error", "Para incluir receitas é necessário existirem categorias.");
            }
            ViewBag.Categorias = categorias;
            return View();
        }

        // POST: Receita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReceitaInsertDto dto)
        {
            try
            {    
                _service.Create(dto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Receita/Edit/XXXXXXX-XXXXXXX-XXXXXXX-XXXXXXXX
        public ActionResult Edit(Guid codigo)
        {
            var receita = _service.FindByCodeEditDto(codigo);
            
            var categorias = new SelectList(
                _categService.List(new Domain.Filters.CategoriaFilter()),
                "Codigo", "Nome", receita.Codigo);
            ViewBag.Categorias = categorias;
            try
            {
                ViewBag.Imagem = _service.FindImageByCode(codigo);
                receita.Imagem = ViewBag.Imagem;
            }
            catch (Exception)
            {
            }
            return View(receita);
        }

        // POST: Receita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReceitaEditDto receita)
        {
            try
            {
                _service.Update(receita);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Receita/Delete/XXXXXXX-XXXXXXX-XXXXXXX-XXXXXXXX
        public ActionResult Delete(Guid codigo)
        {
            var receita = _service.FindByCode(codigo);
            try
            {
                ViewBag.Imagem = _service.FindImageByCode(codigo);
            }
            catch(Exception)
            {
            }
            return View(receita);
        }

        // POST: Categoria/Delete/XXXXXXX-XXXXXXX-XXXXXXX-XXXXXXXXX
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ReceitaDto receita)
        {
            try
            {
                _service.Remove(receita.Codigo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}