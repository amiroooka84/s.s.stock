using dal.User.dl_Basket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bll.User.bl_Basket
{
    public class bl_Basket
    {
        dl_Basket dl_Basket = new dl_Basket();
        public bool InventoryNumberProduct(long number, long attrid, long basket)
        {
            return dl_Basket.InventoryNumberProduct(number, attrid , basket);
        }

        public async Task<bool> DeleteBasket(long id , string UserId)
        {
            return await dl_Basket.DeleteBasket(id , UserId);
        }

        public bool ExistInBasket(long IdProduct, string UserId)
        {
            return dl_Basket.ExistInBasket(IdProduct, UserId);
        }
    }
}
