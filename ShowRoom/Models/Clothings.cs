using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShowRoom.Models
{
    public class Clothings
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Clothing Category")]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Category { get; set; }

        [Display(Name = "Clothing Name")]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        [Display(Name = "Clothing Brand")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Brand { get; set; }

        [Display(Name = "Clothing Description")]
        [StringLength(500, MinimumLength = 1)]
        [Required]
        public string Description { get; set; }

        [System.ComponentModel.Bindable(true)]
        public virtual string ImageUrl { get; set; }

        public ICollection<ClothingOrders> ClothingOrders { get; set; }
    }
}
