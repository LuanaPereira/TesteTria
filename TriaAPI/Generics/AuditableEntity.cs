
using System;
using System.Collections.Generic;
using System.Text;
using TriaAPI.Interfaces;

namespace TriaAPI.Generics
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public bool? Ativo { get; set; }
        public bool? Excluido { get; set; }
    }
}
