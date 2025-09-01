using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Entities
{
    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int? CustomerID { get; set; }
        public int SellerID { get; set; }
        public decimal Total { get; set; }
    }
}
