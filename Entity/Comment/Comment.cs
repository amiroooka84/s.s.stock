using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Comment
{
    public class Comment
    {
        public long id { get; set; }
        public long Productid { get; set; }
        public string Userid { get; set; }
        public string TextComment { get; set; }
        public string Date { get; set; }
    }
}
