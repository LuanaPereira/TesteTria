using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriaAPI.Generics;

namespace TriaAPI.Models
{
    public class ProdutoServico : AuditableEntity<int>
    {
        public string Descricao { get; set; }
    }
}
