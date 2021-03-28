using System;
using System.ComponentModel.DataAnnotations;

namespace skillerator.Models{
    public class SixMonthsPlusOutStay{
        [Display(Name = "von")]public DateTime From {get; set;}
        [Display(Name = "bis")]public DateTime To {get; set;}
        [Display(Name = "in")]public string Country {get; set;}
    }
}