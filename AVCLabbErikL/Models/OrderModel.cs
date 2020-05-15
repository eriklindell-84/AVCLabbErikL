using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AVCLabbErikL.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public Guid UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }
    }
    
}
