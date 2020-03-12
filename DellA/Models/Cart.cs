using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DellA.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public int productID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}