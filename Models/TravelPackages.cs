using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Models
{
    public class TravelPackages
    {
        [Key]

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? Price { get; set; }

        public List<string> ImageUrl { get; set; } = new List<string>();

        public bool FreeCancellation {get; set;} = false;

        public bool ReserveNow {get; set;} = false;

        // public int? UserId { get; set; }

        // public User? User { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}