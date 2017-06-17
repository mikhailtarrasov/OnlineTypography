using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vera.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Тип работ")]
        public string JobTitle { get; set; }
        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }
        [Display(Name = "Валюта")]
        public string CurrencyName { get; set; }
        [Display(Name = "Курс валюты")]
        public decimal CurrencyRate { get; set; }
        public int DependencyId { get; set; }
        [Display(Name = "Цена на")]
        public string DependencyName { get; set; }
        [Display(Name = "Цена на")]
        public SelectList Dependencies { get; set; }
    }
}