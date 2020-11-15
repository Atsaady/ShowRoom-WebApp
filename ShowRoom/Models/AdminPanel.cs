using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShowRoom.Models;


namespace ShowRoom.Models
{
    public class AdminPanel
    {
        [Key]
        public int Id { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public IEnumerable<Clothings> Clothings { get; set; }
    }
}