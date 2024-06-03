using dal.User.dl_Comments;
using Entity.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace bll.User.bl_Comments
{
    public class bl_Comments
    {
        dl_Comments dl_Comments = new dl_Comments();
        public bool AddComment(Comment comment)
        {
            return dl_Comments.AddComment(comment);
        }
        public bool DeleteComment(long commentid, string Userid)
        {
            return dl_Comments.DeleteComment(commentid , Userid);
        }
        public List<Comment> ReadComments(long Productid)
        {
            return dl_Comments.ReadComments(Productid);
        }
    }
}
