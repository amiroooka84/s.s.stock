using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Basket
{
    public class Basket
    {
        public long id { get; set; }
        public long ProductId { get; set; }
        public long AttributeId { get; set; }
        public string UserId { get; set; }
        public int Number { get; set; }
    }
}
