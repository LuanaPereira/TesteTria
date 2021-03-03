using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriaAPI.Interfaces;
using TriaAPI.Models;

namespace TriaAPI.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IRepositorio<Cliente>
    {
        public ClienteRepositorio(DbContext context) : base(context)
        {

        }

    }
}
