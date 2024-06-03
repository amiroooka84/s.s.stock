using bll.Admin.bl_Banner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Shop_Stationery.Controllers.Admin.PagesManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class PagesManagementController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;


        public PagesManagementController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /////////////////
        #region PagesManagement
        [Route("/PagesManagement")]
        public IActionResult PagesManagement()
        {
            bl_Banner bl_Banner = new bl_Banner();
            ViewBag.ListBannerLayout = bl_Banner.ReadBanner();
            return View();
        }
        #endregion
        /////////////////

        /////////////////
        #region AddBanner
        [Route("/PagesManagement/AddBanner")]
        [HttpPost]
        public IActionResult AddBanner(IFormFile File , string link)
        {
            if (File == null) return Redirect("/PagesManagement");
            bl_Banner bl_Banner = new bl_Banner();
            if (File.ContentType.Contains("image"))
            {
                var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-Banners\\Img-Banner-Layout\\" + File.FileName;
                using System.IO.FileStream f = System.IO.File.Create(path);
                File.CopyTo(f);
                bl_Banner.AddBanner(path.Substring(52), link);
                return Redirect("/PagesManagement");
            }
            else
            {
                return Redirect("/PagesManagement");
            }
        }
        #endregion
        /////////////////

        /////////////////
        #region DeleteBanner
        [Route("/PagesManagement/DeleteBanner")]
        [HttpPost]
        public IActionResult DeleteBanner(string NameFile , int Bannerid)
        {
            bl_Banner bl_Banner = new bl_Banner();
            bl_Banner.DeleteBanner(Bannerid);
            if (System.IO.File.Exists(_hostingEnvironment.WebRootPath + "\\Images\\Img-Banners\\Img-Banner-layout\\" + NameFile))
            {
                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "\\Images\\Img-Banners\\Img-Banner-layout\\" + NameFile);
                return Redirect("/PagesManagement");
            }
            else
            {
                return Redirect("/PagesManagement");
            }
        }
        #endregion
        /////////////////

    }
}
