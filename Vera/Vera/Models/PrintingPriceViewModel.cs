using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vera.Models
{
    public class PrintingPriceViewModel
    {
        public int? Id { get; set; }
        public int? FormatId { get; set; }
        //public int? FormingTypeId { get; set; }
        public int? ColorfulnessId { get; set; }
        public decimal? NewCost { get; set; }
    }
}