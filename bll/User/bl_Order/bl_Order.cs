using dal.User.dl_Order;
using Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bll.User.bl_Order
{
    public class bl_Order
    {
        dl_Order dl_Order = new dl_Order();
        public async Task<long> OrderRegistration(Order order, List<ProductOrder> productOrders)
        {
            return await dl_Order.OrderRegistration(order, productOrders);
        }

        public List<Order> ReadOrders(string Userid)
        {
            return dl_Order.ReadOrders(Userid);
        }

        public List<ProductOrder> ReadProductOrders(string Userid)
        {
            return dl_Order.ReadProductOrders(Userid);
        }

        public bool IsFinallyOrder(long orderId, long ref_id)
        {
            return dl_Order.IsFinallyOrder(orderId , ref_id);
        }
    }
}
