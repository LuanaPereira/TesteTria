using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriaAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<ProdutoServico>().ToTable("ProdutoServico");
           
        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ProdutoServico> ProdutoServico { get; set; }
     

    }
}

