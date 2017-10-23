using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Project56_new.Models

{
    public  class Itms {
        public int id {get;set;}
        public string description {get;set;}       
        public string long_description {get;set;}
        public int category_id {get;set;}
        public float price {get;set;}
        public string photo_url {get;set;}
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}
        }  
    public  class ItmCategories {
        public int id {get;set;}
        public string description {get;set;}      
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}
    }  
          
    
}