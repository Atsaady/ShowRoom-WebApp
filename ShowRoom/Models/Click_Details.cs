using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowRoom.Models
{
    public class Click_Details
    {
        public int Id { get; set; }
        public Account Account { get; set; }

        public Clothings Clothings { get; set; }

        public DateTime ClickTime { get; set; }
    }
}
