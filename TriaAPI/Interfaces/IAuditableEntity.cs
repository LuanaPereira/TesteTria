using System;
using System.Collections.Generic;
using System.Text;

namespace TriaAPI.Interfaces
{
    interface IAuditableEntity
    {
        bool? Ativo { get; set; }
        bool? Excluido { get; set; }
    }
}
