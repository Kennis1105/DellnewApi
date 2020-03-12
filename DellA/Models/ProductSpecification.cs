using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DellA.Models
{
    public class ProductSpecifications
    {
        public int Id { get; set; }
        public int productID { get; set; }
        public string typeSpec { get; set; }
        public string specs { get; set; }
        
    }
}