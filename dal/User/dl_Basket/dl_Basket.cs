using Entity;
using Entity.AttributeProduct;
using Entity.Basket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dal.User.dl_Basket
{
    public class dl_Basket
    {
        ////////////
        #region InventoryNumberProduct
        public bool InventoryNumberProduct(long number, long attrid, long basket)
        {
            db db = new db();
            AttributeProduct attributeProduct = new AttributeProduct();
            foreach (var item in db.AttributeProducts)
            {
                if (item.id == attrid)
                {
                    attributeProduct = item;
                }
            }
            if (number <= attributeProduct.Number)
            {
                foreach (var item in db.Baskets)
                {
                    if (item.id == basket)
                    {
                        item.Number = Convert.ToInt32(number);
                    }
                }
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        ////////////

        ////////////
        #region ExistInBasket
        public bool ExistInBasket(long IdProduct, string UserId)
        {
            db db = new db();
            Basket basket = new Basket();
            basket = null;
            foreach (var item in db.Baskets)
            {
                if (UserId == item.UserId && IdProduct == item.ProductId)
                {
                    basket = item;
                }
            }
            if (basket != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        ////////////

        ////////////
        #region DeleteBasket
        public async Task<bool> DeleteBasket(long id, string UserId)
        {
            db db = new db();
            Basket basket = new Basket();
            await foreach (var item in db.Baskets)
            {
                if (item.UserId == UserId && item.id == id)
                {
                    basket = item;
                }
            }
            if (basket != null)
            {
                db.Baskets.Remove(basket);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        ////////////
    }
}
