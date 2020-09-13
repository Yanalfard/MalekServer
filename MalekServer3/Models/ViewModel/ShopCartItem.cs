using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalekServer3.Models.ViewModel
{
    public class ShopCartItem
    {
        public int ProductID { get; set; }
        public int Count { get; set; }
    }
    public class ShopCartViewModel
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }
        public long Sum { get; set; }
        public string ImageName { get; set; }

    }

}