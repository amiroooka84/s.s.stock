using dal.Admin.dl_Order;
using Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace bll.Admin.bl_Order
{
    public class bl_Order
    {
        dl_Order dl_Order = new dl_Order();
        public List<Order> ReadOrder()
        {
            return dl_Order.ReadOrder();
        }

        public List<Order> SearchOrderByCode(long orderCode)
        {
           return dl_Order.SearchOrderByCode(orderCode);
        }

        public Order DetailsOrder(long orderId)
        {
            return dl_Order.DetailsOrder(orderId);
        }

        public List<ProductOrder> ProductsOrder(long orderId)
        {
            return dl_Order.ProductsOrder(orderId);
        }

        public void UpdateStateOrder(long orderId, Order.state state)
        {
             dl_Order.UpdateStateOrder(orderId , state);
        }

        public List<Order> SearchOrderByNumber(string number)
        {
            return dl_Order.SearchOrderByNumber(number);
        }

        public List<Order> SearchOrder(long orderCode, string number)
        {
            return dl_Order.SearchOrder(orderCode , number);
        }
    }
}
