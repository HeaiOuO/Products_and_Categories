using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProAndCate.Models
{
    public class Association
    {
        [Key]
        [Required]
        public int AssociationId{get;set;}

        [Required]
        [Display(Name="Add Category:")]
        public int CategoryId{get;set;}

        [Required]
        public int ProductId{get;set;}
        public Product product{get;set;}
        public Category category{get;set;}
    public class productAssociationInfo
    { 
        public Association association{get;set;}=new Association();
        public Product product{get;set;}
        public List<Category> allCategory{get;set;}
    }
    public class categoryAssociationInfo
    { 
        public Association association{get;set;}=new Association();
        public Category category{get;set;}
        public List<Product> allProducts{get;set;}
    }


    }
}