using Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelAdmin.ViewModelCategory
{
    public class ViewModelAddCategory
    {
        [Required(ErrorMessage = "نام دسته خالی است")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "نام دسته*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "عکس خالی است")]
        [DataType(DataType.Upload)]
        [Display(Name = "عکسه دسته*")]
        public IFormFile Image { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "دسته")]
        public int Category { get; set; }
    }
    public class ViewModelCategory
    {
        public ViewModelCategory(ViewModelAddCategory _Category, List<Category> _Categories)
        {
            Category = _Category;
            Categories = _Categories;
        }
        public ViewModelAddCategory Category { get; set; }
        public List<Category> Categories { get; set; }
    }

    public
        class ViewModelNameCategory
    {
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "جستجو")]
        public string Name { get; set; }
    }
    public class ViewModelSearchCategory
    {
        public ViewModelSearchCategory(ViewModelNameCategory nameCategory, List<Category> categories)
        {
            NameCategory = nameCategory;
            Categories = categories;
        }
        public ViewModelNameCategory NameCategory { get; set; }
        public List<Category> Categories { get; set; }
    }

}
