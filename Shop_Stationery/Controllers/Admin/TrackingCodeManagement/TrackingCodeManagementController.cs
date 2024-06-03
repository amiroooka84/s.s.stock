using bll.Admin.bl_TrackingCode;
using Entity.TrackingCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Stationery.Models.Classes.ClassesUser.ConvertShamsiToMiladi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Controllers.Admin.TrackingCodeManagement
{
    [Authorize(Policy = "AdminOnly")]
    public class TrackingCodeManagementController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;


        public TrackingCodeManagementController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [Route("/TrackingCodeManagement")]
        public IActionResult TrackingCode()
        {
            bl_TrackingCode bl_TrackingCode = new bl_TrackingCode();
            ViewBag.TrackingCodes = bl_TrackingCode.ReadTrackingCodes();
            return View("TrackingCodeManagement");
        }

        /////////////////
        #region AddTrackingCode
        [Route("/TrackingCodeManagement/AddTrackingCode")]
        [HttpPost]
        public IActionResult AddTrackingCode(IFormFile File)
        {
            if (File == null) return Redirect("/PagesManagement");
            bl_TrackingCode bl_TrackingCode = new bl_TrackingCode();
            if (File.ContentType.Contains("image"))
            {
                var path = _hostingEnvironment.WebRootPath + "\\Images\\Img-TrackingCode\\" + File.FileName;
                using System.IO.FileStream f = System.IO.File.Create(path);
                File.CopyTo(f);
                bl_TrackingCode.AddTrackingCode(path.Substring(52), ConvertDateTime.ConvertMiladiToShamsi(DateTime.Today, "yyyy/MM/dd"));
                return Redirect("/TrackingCodeManagement");
            }
            else
            {
                return Redirect("/TrackingCodeManagement");
            }
        }
        #endregion
        /////////////////

        /////////////////
        #region DeleteTrackingCode
        [Route("/TrackingCodeManagement/DeleteTrackingCode")]
        public IActionResult DeleteTrackingCode(long TrackingCodeid)
        {
            bl_TrackingCode bl_TrackingCode = new bl_TrackingCode();
            string path = bl_TrackingCode.DeleteTrackingCode(TrackingCodeid);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return Redirect("/TrackingCodeManagement");
        }
        #endregion
        /////////////////
    }
}
