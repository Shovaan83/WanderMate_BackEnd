using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Models
{
    public class User
    {
        [Key]

        public int Id { get; set; }

        public string Role {get; set;} = string.Empty;

        public string Email {get; set;} = string.Empty;

        public string Username {get; set;} = string.Empty;

        public string? Password {get; set;} = string.Empty;

        public string? ConfrimPassword {get; set;} = string.Empty;

        // public string CoverImg {get; set;} = string.Empty;

        // public string Bio {get; set;} = string.Empty;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        // public ICollection<TravelPackages> TravelPackages { get; set; } = new List<TravelPackages>();
    }
}