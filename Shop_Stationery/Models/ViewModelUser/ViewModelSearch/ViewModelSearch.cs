using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.ViewModelUser.ViewModelSearch
{
    public class ViewModelSearch
    {
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "جستجو")]
        public string Search { get; set; }
    }

    public class ViewModelSearchByFilter
    {
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "جستجو")]
        public string Search { get; set; }

        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "مرتب سازی")]
        public string Order { get; set; }

        [Display(Name = "دسته بندی")]
        public List<string> Categories { get; set; }

        [Display(Name = "صفحه")]
        public int Page { get; set; }
    }
}
