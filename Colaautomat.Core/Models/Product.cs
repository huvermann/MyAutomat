using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Models
{
    public class Product
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsInStock()
        {
            return (Count > 0);
        }
    }
}
