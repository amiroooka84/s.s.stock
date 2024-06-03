using dal.Admin.dl_User;
using Entity.UserApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace bll.Admin.bl_User
{
    public class bl_User
    {
        dl_User dl_User = new dl_User();
        public List<userapp.UserApp> ReadUsers()
        {
            return dl_User.ReadUsers();
        }
    }
}
