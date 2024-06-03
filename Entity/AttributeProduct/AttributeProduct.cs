using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.AttributeProduct
{
    public class AttributeProduct
    {
        public int id{ get; set; }
        //Size Or Color
        public string Size{ get; set; }
        public string Color{ get; set; }
        //If TypeAttribute == Color This prop should not be empty
        public string ColorCode{ get; set; }
        //Number of products with this feature
        public int Number{ get; set; }
        public long ProductId { get; set; }
    }
}
