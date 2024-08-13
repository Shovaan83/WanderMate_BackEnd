using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Models
{
        [Table("Hotel")]
    public class Hotel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;    

        [Required]
        public string Description {get; set; } = String.Empty;

        public List<string> ImageUrl { get; set; } = new List<string>();

        [Required]
        public int? Price { get; set; } 

        public bool FreeCancellation {get; set;} = false;

        public bool ReserveNow {get; set;} = false;

        // public bool IsDeleted {get; set; } = false;

        public ICollection<Review> Reviews {get; set;} = new List<Review>();

        // public ICollection<Booking> Bookings {get; set;} = new List<Booking>();

    }
}