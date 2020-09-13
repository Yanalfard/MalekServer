using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalekServer3.Models.ViewModel
{
    public class ShowProduct
    {

        public int id { get; set; }
        public string product { get; set; }
        public string cat { get; set; }
        public string Date { get; set; }
        public long AfterDiscount { get; set; }
        public int Count { get; set; }
        public long TotalAfterDiscount { get; set; }
        public int AllCount { get; set; }
        public long AllTotalAfterDiscount { get; set; }
    }
   

}