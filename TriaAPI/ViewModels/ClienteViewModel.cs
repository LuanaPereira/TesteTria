using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriaAPI.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Conversa { get; set; }
        public DateTime Data { get; set; }
        public DateTime HoraAtendimento { get; set; }
        public int idProduto { get; set; }
    }
}
