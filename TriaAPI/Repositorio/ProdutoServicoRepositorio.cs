using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriaAPI.Interfaces;
using TriaAPI.Models;

namespace TriaAPI.Repositorio
{
    public class ProdutoServicoRepositorio : Repositorio<ProdutoServico>, IRepositorio<ProdutoServico>
    {
        public ProdutoServicoRepositorio(DbContext context) : base(context)
        {

        }

    }
}
