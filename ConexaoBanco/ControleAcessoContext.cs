using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleAcesso.Models;

namespace ControleAcesso.ConexaoBanco
{
    public class ControleAcessoContext : DbContext
    {
        public ControleAcessoContext(DbContextOptions<ControleAcessoContext> options)
            : base(options)
        {
        }

        public DbSet<visitantes> Visitante { get; set; }
    }
}
