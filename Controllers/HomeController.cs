using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControleAcesso.Models;
using ControleAcesso.ConexaoBanco;

namespace ControleAcesso.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ControleAcessoContext _context;

        
        
          
        public HomeController(ILogger<HomeController> logger, ControleAcessoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index (visitantes visitante)
        {

            try
            {
                 _context.Add(visitante);
                _context.SaveChanges();
                return RedirectToAction("Index", "visitantes");
            }
            catch(Exception error)
            {
                return BadRequest("Erro ao salvar o visitante");
            }

           
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
