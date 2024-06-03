using Entity;
using Entity.ProductAndShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.Admin.dl_Shop
{
    public class dl_Shop
    {
        ////////////
        #region Reads
        public Shop ReadShopById(int id)
        {
            db db = new db();
            Shop shop = new Shop();
            foreach (var item in db.Shops)
            {
                if (item.id == id)
                {
                    shop = item;
                }
            }
            return shop;
        }
        public List<Shop> ReadShop()
        {
            db db = new db();
            return db.Shops.ToList();
        }
        #endregion
        ////////////

        ////////////
        #region Exists
        public bool ExistShop(long Shopid)
        {
            db db = new db();
            int i = 0;
            foreach (var item in db.Shops)
            {
                if (item.id == Shopid)
                {
                    i = 1;
                    break;
                }
            }
            if (i > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        ////////////

        ////////////
        #region Add
        public bool AddShop(Shop shop)
        {
            db db = new db();
            db.Shops.Add(shop);
            db.SaveChanges();
            return true;
        }
        #endregion
        ////////////

        ////////////
        #region Delete
        public bool DeleteShop(int id)
        {
            db db = new db();
            db.Shops.Remove(ReadShopById(id));
            return true;
        }
        #endregion
        ////////////

        //Start Product And Shop
        #region Product And Shop
        public List<ProductAndShop> ReadShopAndProduct()
        {
            db db = new db();
            return db.ProductAndShop.ToList();
        }
        #endregion
        //End Product And Shop

    }
}
