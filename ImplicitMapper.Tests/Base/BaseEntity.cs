using ImplicitMapper.Tests.Classes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ImplicitMapper.Tests.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public T CloneTo<T>()
        {
            var obj = Activator.CreateInstance<T>();
            Cast.Clone(this, obj);
            return obj;
        }

        public void CloneBy(object obj)
        {
            Cast.Clone(obj, this);
        }
    }
}