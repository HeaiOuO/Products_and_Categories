using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProAndCate.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Name must be 2 characters or longer!")]
        public string Name { get; set; }

        [Required]
        [Display(Description = "Description")]
        [MinLength(5, ErrorMessage = "Description must be 5 characters or longer!")]

        public string Description { get; set; }

        [Required]
        [Range(1,Int32.MaxValue,ErrorMessage ="Must be a valid Price")]
        public decimal Price {get;set;}
        public DateTime CreatAt {get;set;} = DateTime.Now;
        public DateTime UpdatAt {get;set;} = DateTime.Now;
        public List<Association> categories{get;set;}
        
    }
    public class productPackageInfo
    {
        public Product product{get;set;}=new Product();
        public List<Product> allProducts{get;set;}
    }

}