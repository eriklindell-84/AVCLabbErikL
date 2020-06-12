using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVCLabbErikL.Models
{
    public class OrderModel
    {
        public OrderModel()
        { }
        public int Id { get; set; }
        public Guid UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderAmount { get; set; }

    }
    
}
