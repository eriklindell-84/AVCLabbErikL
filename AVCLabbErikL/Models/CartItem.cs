using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AVCLabbErikL.Models
{
    public class CartItem
    {
        public virtual OrderModel Orders { get; set; }
        public virtual ProductModel Products { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
