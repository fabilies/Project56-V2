//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System;
//using System.Linq;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

////using Microsoft.Extensions.WebEncoders;
//namespace Project56_new.Models

//  {
   
   
//    public  class Users {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int id {get;set;}
//        public string email  {get;set;}    
//        public string password {get;set;}   
//        public int user_level {get;set;}

//       /// public string fullname {get;set;}   
        
//        // Error while updating database 
//        // Timezone is not set --> FIX
//       // public DateTime dt_birth {get;set;}
//       // public string gender {get;set;}

//        public int l_show {get;set;}

        
//        public DateTime dt_created {get{ return DateTime.Now;}}

//        public DateTime dt_modified {get{ return DateTime.Now;}}
//        // adres details
//      //  public string a_zipcode {get;set;}

//     //   public string a_adres {get;set;}
//      //   public string a_city{get;set;}

        
          
//    }
//}