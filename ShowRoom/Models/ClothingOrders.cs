using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShowRoom.Models
{
    public class ClothingOrders
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Clothings Clothings { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int Count { get; set; }
    }
}