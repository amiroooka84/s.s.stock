using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Shop
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string NumberPhone { get; set; }
        public int PostingDay { get; set; }
    }
}
