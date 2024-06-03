using Entity.UserApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.Admin.dl_User
{
    public class dl_User
    {
        public List<userapp.UserApp> ReadUsers()
        {
            db db = new db();
            return db.Users.ToList();
        }
    }
}
