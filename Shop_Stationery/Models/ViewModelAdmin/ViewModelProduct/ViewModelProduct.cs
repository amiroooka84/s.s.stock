using Entity;
using Entity.AttributeProduct;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelAdmin.ViewModelProduct
{
    public class ViewModelAddProduct
    {
        [Required(ErrorMessage = "نام کالا خالی است")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "نام کالا*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "نامک کالا خالی است")]
        [MaxLength(400)]
        [DataType(DataType.Text)]
        [Display(Name = "نامک کالا*")]
        public string Slack { get; set; }
        [Required(ErrorMessage = "کد کالا خالی است")]
        [Display(Name = "کد کالا*")]
        public string Code { get; set; }
        [Required(ErrorMessage = "قیمت کالا خالی است")]
        [Display(Name = "قیمت کالا*")]
        public string Price { get; set; }
        [Display(Name = "تخفیف کالا")]
        public string Discount { get; set; }
        [Required(ErrorMessage = "تعداد کالا خالی است")]
        [Display(Name = "تعداد کالا*")]
        public string Number { get; set; }
        [Display(Name = "آیدی دسته")]
        public long Categoryid { get; set; }
        [Required(ErrorMessage = "توضیحات کالا خالی است")]
        [DataType(DataType.Text)]
        [Display(Name = "توضیحات*")]
        public string Discription { get; set; }
        [Required(ErrorMessage = "توضیحات کالا خالی است")]
        [DataType(DataType.Text)]
        [Display(Name = "مشخصات*(بین هرکدام نقطه بگذارید)")]
        public string Specifications { get; set; }
        [Required(ErrorMessage = "تصویر اصلی خالی است")]
        [DataType(DataType.Upload)]
        [Display(Name = "تصویر اصلی")]
        public IFormFile Image { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "تصاویر")]
        public List<IFormFile> Images { get; set; }
        [Display(Name = "فروشگاه ها")]
        public List<long> Shops { get; set; }
        [Display(Name = "ویژگی ها")]
        public List<AttributeProduct> Attributes { get; set; }
    }

    public class ViewModelProduct
    {
        public ViewModelProduct(ViewModelAddProduct product, List<Product> products)
        {
            Product = product;
            Products = products;
        }
        public ViewModelAddProduct Product { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ViewModelNameProduct
    {
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "نام کالا*")]
        public string Name { get; set; }
    }
}
