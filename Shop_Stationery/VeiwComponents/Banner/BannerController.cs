using bll.Admin.bl_Banner;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.VeiwComponents.User.Banner
{
    public class BannerViewComponent : ViewComponent
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public BannerViewComponent(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bl_Banner bl_Banner = new bl_Banner();
            List<Entity.Banner> a = new List<Entity.Banner>();
            a = bl_Banner.ReadBanner();
            return View("Banner", a.ToList());
        }
    }
}
