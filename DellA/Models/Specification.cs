using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DellA.Models
{
    public class Specification
    {
        public int Id { get; set; }
        public int productID { get; set; }
        public string Core { get; set; }
        public string Windows { get; set; }
        public string Graphics { get; set; }
        public string Ram { get; set; }
        public string Drive { get; set; }
    }
}