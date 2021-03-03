using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriaAPI.Generics;

namespace TriaAPI.Models
{
    public class Cliente : AuditableEntity<int>
    {
        public string Empresa { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Conversa { get; set; }
        public DateTime Data { get; set; }
        public DateTime HoraAtendimento { get; set; }
        public ProdutoServico Produto { get; set; }
    }
}
