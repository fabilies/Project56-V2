using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project56_new.Models

{
    public  class Itms {
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Product naam")]
        public string description { get; set; }
        [Display(Name = "Product omschrijving")]
        public string long_description { get; set; }
        [Display(Name = "Categorie")]
        public int category_id { get; set; }
        [Display(Name = "Prijs")]
        public float price { get; set; }
        [Display(Name = "Afbeelding")]
        public string photo_url { get; set; }
        [Display(Name = "Product status")]
        public int l_show { get; set; }
        [Display(Name = "Voorraad")]
        public int itm_quantity { get; set; }
        [Display(Name = "Aanmaakdatum")]
        public DateTime dt_created { get; set; }
        [Display(Name = "Wijzigingsdatum")]
        public DateTime dt_modified { get; set; }

    }
    public  class ItmCategories {
        public int id {get;set;}
        public string description {get;set;}      
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}
    }  
          
    
}