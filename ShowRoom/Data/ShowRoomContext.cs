using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowRoom.Models;

namespace ShowRoom.Data
{
    public class ShowRoomContext : DbContext
    {
        public ShowRoomContext(DbContextOptions<ShowRoomContext> options)
            : base(options)
        {
        }

        public DbSet<ShowRoom.Models.Account> Account { get; set; }

        public DbSet<ShowRoom.Models.Order> Order { get; set; }

        public DbSet<ShowRoom.Models.Clothings> Clothings { get; set; }

        public DbSet<ShowRoom.Models.ClothingOrders> ClothingOrders { get; set; }

        public DbSet<ShowRoom.Models.AdminPanel> Admin { get; set; }

        public DbSet<ShowRoom.Models.Cart> Cart { get; set; }

        public DbSet<ShowRoom.Models.Click_Details> Click_Details { get; set; }

        public DbSet<ShowRoom.Models.Contact> Contact { get; set; }
    }
}