﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vera.Models
{
    public class PrintViewModel
    {
        public SelectList Colorfulness { get; set; }
        public SelectList Paper { get; set; }
    }

    public class FormingTypeViewModel : PrintViewModel
    {
        public string FormingTypeName { get; set; }

    }

    public class CalculatorViewModel : PrintViewModel
    {
        public SelectList Format { get; set; }
        public SelectList FormingType { get; set; }
        public SelectList Cardboard { get; set; }
        public SelectList BindingMaterials { get; set; }
    }
}