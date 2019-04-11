using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProAndCate.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Name must be 2 characters or longer!")]
        public string Name { get; set; }
        public DateTime CreatAt {get;set;} = DateTime.Now;
        public DateTime UpdatAt {get;set;} = DateTime.Now;

        public List<Association> products{get;set;}
    }
    public class categoryPackageInfo
    {
        public Category category{get;set;}=new Category();
        public List<Category> allCategory{get;set;}
    }
}