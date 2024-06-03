using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Address;
using Entity.UserApp;
using static Entity.UserApp.userapp;

namespace dal.User.dl_Address
{
    public class dl_Address
    {
        /////////////
        #region AddAddress
        public bool AddAddress(string address, UserApp user)
        {
            db db = new db();
            Address a = new Address();
            a.address = address;
            a.UserAppId = user.Id;
            try
            {
                db.Addresses.Add(a);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        /////////////

        /////////////
        #region DeleteAddress
        public bool DeleteAddress(long id, UserApp user)
        {
            db db = new db();
            Address address = new Address();
            foreach (var item in db.Addresses)
            {
                if (item.id == id)
                {
                    address = item;
                }
            }
            if (address != null)
            {
                if (address.UserAppId == user.Id)
                {
                    try
                    {
                        db.Addresses.RemoveRange(address);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        #endregion
        /////////////

        /////////////
        #region ReadAddress
        public List<Address> ReadAddress(UserApp user)
        {
            db db = new db();
            List<Address> La = new List<Address>();
            foreach (var item in db.Addresses)
            {
                if (user.Id == item.UserAppId)
                {
                    La.Add(item);
                }
            }
            return La;
        }
        #endregion
        /////////////

    }
}