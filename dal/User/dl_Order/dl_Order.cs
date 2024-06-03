using Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dal.User.dl_Order
{
    public class dl_Order
    {
        /////////////
        #region ExistCodeOrder
        bool ExistCodeOrder(long Code)
        {
            db db = new db();
            bool ExistCode = false;
            foreach (var item in db.Orders)
            {
                if (item.Code == Code)
                {
                    ExistCode = true;
                    break;
                }
            }
            return false;
        }

        #endregion
        /////////////

        /////////////
        #region OrderRegistration
        public async Task<long> OrderRegistration(Order order, List<ProductOrder> productOrders)
        {
            db db = new db();
            Random random = new Random();
            order.Code = random.Next(100000000, 999999999);
            while (ExistCodeOrder(order.Code))
            {
                order.Code = random.Next(100000000, 999999999);
            }
            var Order = db.Orders.Add(order);
            db.SaveChanges();
            foreach (var item in productOrders)
            {
                item.OrderId = Order.Entity.id;
                db.ProductOrders.Add(item);
            }
            await foreach (var item in db.Products)
            {
                foreach (var product in productOrders)
                {
                    if (item.Code == product.Code)
                    {
                        item.Number--;
                    }
                }
            }
            foreach (var item in db.AttributeProducts)
            {
                foreach (var productOrder in productOrders)
                {
                    if (item.id == productOrder.AttrId)
                    {
                        item.Number--;
                    }
                }
            }
            await foreach (var item in db.Baskets)
            {
                if (item.UserId == order.UserId)
                {
                    db.Baskets.Remove(item);
                }
            }
            db.SaveChanges();
            return Order.Entity.id; ;
        }



        #endregion
        /////////////

        /////////////
        #region IsFinallyOrder
        public bool IsFinallyOrder(long orderId, long ref_id)
        {
            bool a = false;
            db db = new db();
            foreach (var item in db.Orders)
            {
                if (item.id == orderId)
                {
                    item.IsFinally = true;
                    //item.RefId = ref_id;
                    a = true;
                    break;
                }
            }
            if (a)
            {
                db.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion
        /////////////


        /////////////
        #region ReadProductOrders
        public List<ProductOrder> ReadProductOrders(string userid)
        {
            db db = new db();
            List<ProductOrder> productOrders = new List<ProductOrder>();
            foreach (var item in db.ProductOrders)
            {
                if (item.UserId == userid)
                {
                    productOrders.Add(item);
                }
            }
            return productOrders;
        }

        #endregion
        /////////////

        /////////////
        #region ReadOrders
        public List<Order> ReadOrders(string userid)
        {
            db db = new db();
            List<Order> orders = new List<Order>();
            foreach (var item in db.Orders)
            {
                if (item.UserId == userid)
                {
                    orders.Add(item);
                }
            }
            return orders;
        }

        #endregion
        /////////////

    }
}
