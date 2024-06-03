using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Order
{
    public class Order
    {
        public long id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public long Code { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public enum state
        {
            Processing , Preparation  , Canceled , Delivered 
        }
        public state State { get; set; }
        public int RefId { get; set; }
        public bool IsFinally { get; set; }
    }


    public class ProductOrder
    {
        public long id { get; set; }
        public string UserId { get; set; }
        public long OrderId{ get; set; }
        public string Name { get; set; }
        public long Code { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public int Number { get; set; }
        public string Color{ get; set; }
        public string Size{ get; set; }
        public string ImagePath { get; set; }
        public long AttrId { get; set; }
    }
}
