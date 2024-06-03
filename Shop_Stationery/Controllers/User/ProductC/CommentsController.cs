using bll.User.bl_Comments;
using Entity.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Stationery.Models.Classes.ClassesUser.ConvertShamsiToMiladi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Entity.UserApp.userapp;

namespace Shop_Stationery.Controllers.User.ProductC
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly UserManager<UserApp> _userManager;

        public CommentsController(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        ////////////////
        #region AddComment
        [Route("/AddComment")]
        [HttpPost]
        public async Task<IActionResult> AddComment(long Productid, string TextComment, string NameProduct)
        {
            if (TextComment.Contains("img") || TextComment.Contains("style") || TextComment.Contains("meta") || TextComment.Contains("script"))
            {
                return Redirect("Product/" + NameProduct);
            }
            bl_Comments bl_Comments = new bl_Comments();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            Comment comment = new Comment()
            {
                Userid = userApp.Id,
                Productid = Productid,
                TextComment = TextComment,
                Date = ConvertDateTime.ConvertMiladiToShamsi(DateTime.Today, "yyyy/MM/dd")
            };
            bool result = bl_Comments.AddComment(comment);
            return Redirect("Product/" + NameProduct);
        }

        #endregion
        ////////////////

        ////////////////
        #region DeleteComment
        [Route("/DeleteComment")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(long Commentid, string NameProduct)
        {
            bl_Comments bl_Comments = new bl_Comments();
            UserApp userApp = new UserApp();
            userApp = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = bl_Comments.DeleteComment(Commentid, userApp.Id);
            return Redirect("Product/" + NameProduct);
        }
        #endregion
        ////////////////

    }
}
