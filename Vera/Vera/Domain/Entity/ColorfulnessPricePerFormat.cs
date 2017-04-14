using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vera.Domain.Entity
{
    public class ColorfulnessPricePerFormat
    {
        public int Id { get; set; }
        public virtual Colorfulness Colorfulness { get; set; }
        public virtual Format Format { get; set; }
        public virtual Price Price { get; set; }
    }
}