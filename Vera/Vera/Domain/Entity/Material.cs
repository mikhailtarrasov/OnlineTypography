using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vera.Domain.Entity
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Price Price { get; set; }
        public virtual MaterialType Type { get; set; }
    }
}