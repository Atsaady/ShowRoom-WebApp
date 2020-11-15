using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShowRoom.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public String Address { get; set; }

        public Account Account { get; set; }

        public DateTime OrderTime { get; set; }

        public String Phone { get; set; }

        public double SumToPay { get; set; }

        public ICollection<ClothingOrders> ClothingOrders { get; set; }
    }
}
