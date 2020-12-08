using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleAcesso.ConexaoBanco;
using ControleAcesso.Models;

namespace ControleAcesso.Controllers
{
    public class visitantesController : Controller
    {
        private readonly ControleAcessoContext _context;

        public visitantesController(ControleAcessoContext context)
        {
            _context = context;
        }

        // GET: visitantes
        public async Task<IActionResult> Index(string nomeVisitante)
        {
            //buscar os dados dos visitantes
            var visitantes = from m in _context.Visitante
                         select m;
            // verifica se a busca é diferente de vazio ou nulo
            if (!String.IsNullOrEmpty(nomeVisitante))
            {
                // busca os visitantes com base na busca
                visitantes = visitantes.Where(s => s.Nome.Contains(nomeVisitante));
            }

            return View(await visitantes.ToListAsync());
        }

        // GET: visitantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitantes = await _context.Visitante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitantes == null)
            {
                return NotFound();
            }

            return View(visitantes);
        }

        // GET: visitantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: visitantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,CPF,RG,Email,Sala,Oberservacao")] visitantes visitantes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitantes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitantes);
        }

        // GET: visitantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitantes = await _context.Visitante.FindAsync(id);
            if (visitantes == null)
            {
                return NotFound();
            }
            return View(visitantes);
        }

        // POST: visitantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,CPF,RG,Email,Sala,Oberservacao")] visitantes visitantes)
        {
            if (id != visitantes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitantes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!visitantesExists(visitantes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(visitantes);
        }

        // GET: visitantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitantes = await _context.Visitante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitantes == null)
            {
                return NotFound();
            }

            return View(visitantes);
        }

        // POST: visitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitantes = await _context.Visitante.FindAsync(id);
            _context.Visitante.Remove(visitantes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool visitantesExists(int id)
        {
            return _context.Visitante.Any(e => e.Id == id);
        }
    }
}
