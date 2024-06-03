using System;
using System.Collections.Generic;
using System.Text;
using dal;
using dal.Admin.dl_Shop;
using Entity;
using Entity.ProductAndShop;

namespace bll.Admin.bl_Shop
{
    public class bl_Shop
    {
        dl_Shop dl_Shop = new dl_Shop();
        public bool ExistShop(long Shopid)
        {
            return dl_Shop.ExistShop(Shopid);
        }

        public List<Shop> ReadShop()
        {
            return dl_Shop.ReadShop();
        }

        public bool AddShop(Shop shop)
        {
            return dl_Shop.AddShop(shop);
        }

        public bool DeleteShop(int id)
        {
            return dl_Shop.DeleteShop(id);
        }

        public List<ProductAndShop> ReadShopAndProduct()
        {
            return dl_Shop.ReadShopAndProduct();
        }
    }
}
