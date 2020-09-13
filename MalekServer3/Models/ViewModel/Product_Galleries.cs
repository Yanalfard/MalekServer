using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalekServer3
{
    public class Product_Galleries
    {
        public int ProductID { get; set; }
        public string Image { get; set; }
        public int ImageId { get; set; }
    }
    public class Product_Property
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PropertyID { get; set; }

    }
}