using System;
using System.Collections.Generic;
using System.Text;
using dal.User.dl_Address;
using Entity.Address;
using static Entity.UserApp.userapp;

namespace bll.User.bl_Address
{
    public class bl_Address
    {
        dl_Address dl_Address = new dl_Address();
        public bool AddAddress(string address , UserApp user)
        {
            return dl_Address.AddAddress(address , user);
        }
        public List<Address> ReadAddress(UserApp user)
        {
            return dl_Address.ReadAddress(user);
        }

        public bool DeleteAddress(long id, UserApp user)
        {
            return dl_Address.DeleteAddress(id , user);
        }
    }
}
