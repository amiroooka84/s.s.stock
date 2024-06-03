using Entity;
using Entity.AttributeProduct;
using Entity.ProductAndShop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelAdmin.ViewModelShop
{
    public class ViewModelAddShop
    {
        [Required(ErrorMessage = "نام فروشگاه خالی است")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "نام فروشگاه*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "آدرس فروشگاه خالی است")]
        [Display(Name = "آدرس فروشگاه*")]
        public string Address { get; set; }
        [Required(ErrorMessage = "شماره فروشگاه خالی است")]
        [MaxLength(11)]
        [MinLength(8)]
        [Display(Name = "شماره فروشگاه*")]
        public string NumberPhone { get; set; }
        [Required(ErrorMessage = "زمان ارسال")]
        [MaxLength(1)]
        [Display(Name = "زمان ارسال فروشگاه*")]
        public string PostingDay { get; set; }
    }


    public class ViewModelProductAndShop
    {
            public ViewModelProductAndShop(Product product, List<ProductAndShop> pas1, List<AttributeProduct> attributeProducts)
            {
                pas = pas1;
                Product = product;
                Attributes = attributeProducts;
            }
            public List<ProductAndShop> pas { get; set; }
            public List<AttributeProduct> Attributes { get; set; }
            public Product Product { get; set; }
    }
}
