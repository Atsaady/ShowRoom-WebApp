using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowRoom.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public Clothings Clothings { get; set; }

        public int Count { get; set; }

        public Account account { get; set; }
    } 
}
