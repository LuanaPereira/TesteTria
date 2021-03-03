using System;
using System.Collections.Generic;
using System.Text;
using TriaAPI.Interfaces;

namespace TriaAPI.Generics
{
    public abstract class BaseEntity
    {

    }
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
