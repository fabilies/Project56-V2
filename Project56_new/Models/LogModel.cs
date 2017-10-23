using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Project56_new.Models

{
    public  class Logs {
        public int id {get;set;}
        public int log_type  {get;set;}    
        public int user_id {get;set;}   
       
        public int l_show {get;set;}
        public DateTime dt_created {get;set;}
        public DateTime dt_modified {get;set;}
    }  
          
}
