using System;
using System.Collections.Generic;
using System.Text;

namespace TriaAPI.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
