using Entity.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace dal.User.dl_Comments
{
    public class dl_Comments
    {
        //////////////
        #region ReadComments
        public List<Comment> ReadComments(long Productid)
        {
            db db = new db();
            List<Comment> comments = new List<Comment>();
            foreach (var item in db.Comments)
            {
                if (Productid == item.Productid)
                {
                    comments.Add(item);
                }
            }
            return comments;
        }
        #endregion
        //////////////

        //////////////
        #region AddComment
        public bool AddComment(Comment comment)
        {
            db db = new db();
            db.Comments.Add(comment);
            db.SaveChanges();
            return true;
        }
        #endregion
        //////////////

        //////////////
        #region DeleteComment
        public bool DeleteComment(long commentid, string userid)
        {
            db db = new db();
            foreach (var item in db.Comments)
            {
                if (commentid == item.id)
                {
                    if (item.Userid == userid)
                    {
                        db.Comments.Remove(item);
                    }
                }
            }
            db.SaveChanges();
            return true;
        }

        #endregion
        //////////////
    }
}
