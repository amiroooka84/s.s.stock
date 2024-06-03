using System;
using System.Collections.Generic;

namespace Entity
{
    public class Product
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Slack { get; set; }       
        public long Code { get; set; }
        public long Price { get; set; }
        public long Discount { get; set; }
        public int Number { get; set; }
        public string Discription { get; set; }
        public string Specifications { get; set; }
        public string ImagePath { get; set; }
        public long Categoryid { get; set; }
    }
}
