using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalekServer3.Models.ObjectClass
{
    public class OcCatagory 
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<OcProduct> Products { get; set; }

        public OcCatagory()
        {
        }

        public OcCatagory(int id, string name, int? parentId, List<OcProduct> products)
        {
            this.id = id;
            Name = name;
            ParentId = parentId;
            Products = products;
        }
    }
}