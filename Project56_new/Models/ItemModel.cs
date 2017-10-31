using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project56_new.Models

{
    public  class Itms {
        public int id {get;set;}
        [Display(Name = "Beschrijving")]
        public string description {get;set;}       
        public string long_description {get;set;}
        public int category_id {get;set;}
        public float price {get;set;}
        public string photo_url {get;set;}
        public int l_show {get;set;}
        public int itm_quantity { get; set; }
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}

        public string ImageName { get; set; }

    }
    public  class ItmCategories {
        public int id {get;set;}
        public string description {get;set;}      
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}
    }  
          
    
}