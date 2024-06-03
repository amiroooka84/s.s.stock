using Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.Admin.dl_Order
{
    public class dl_Order
    {
        /////////////
        #region ReadOrder
        public List<Order> ReadOrder()
        {
            db db = new db();
            return db.Orders.ToList();
        }

        #endregion
        /////////////

        /////////////
        #region SearchOrderByCode
        public List<Order> SearchOrderByCode(long orderCode)
        {
            db db = new db();
            List<Order> order = new List<Order>();
            foreach (var item in db.Orders)
            {
                if (item.Code == orderCode)
                {
                    order.Add(item);
                }
            }
            return order;
        }
        #endregion
        /////////////

        /////////////
        #region ProductsOrder
        public List<ProductOrder> ProductsOrder(long orderId)
        {
            db db = new db();
            List<ProductOrder> productOrders = new List<ProductOrder>();
            foreach (var item in db.ProductOrders)
            {
                if (item.OrderId == orderId)
                {
                    productOrders.Add(item);
                }
            }
            return productOrders;
        }
        #endregion
        /////////////

        /////////////
        #region SearchOrderByNumber
        public List<Order> SearchOrderByNumber(string number)
        {
            db db = new db();
            List<Order> order = new List<Order>();
            foreach (var item in db.Orders)
            {
                if (item.PhoneNumber == number)
                {
                    order.Add(item);
                }
            }
            return order;
        }

        #endregion
        /////////////

        /////////////
        #region SearchOrder
        public List<Order> SearchOrder(long orderCode, string number)
        {
            db db = new db();
            List<Order> order = new List<Order>();
            foreach (var item in db.Orders)
            {
                if (item.PhoneNumber == number && item.Code == orderCode)
                {
                    order.Add(item);
                }
            }
            return order;
        }

        #endregion
        /////////////

        /////////////
        #region UpdateStateOrder
        public void UpdateStateOrder(long orderId, Order.state state)
        {
            db db = new db();
            foreach (var item in db.Orders)
            {
                if (item.id == orderId)
                {
                    item.State = state;
                }
            }
            db.SaveChanges();
        }

        #endregion
        /////////////

        /////////////
        #region DetailsOrder
        public Order DetailsOrder(long orderId)
        {
            db db = new db();
            Order order = new Order();
            foreach (var item in db.Orders)
            {
                if (item.id == orderId)
                {
                    order = item;
                }
            }
            return order;
        }
        #endregion
        /////////////

    }
}
