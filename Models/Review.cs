using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Models
{
    public class Review
    {
        [Key]

        public int? Id { get; set; }

        public string ReviewText { get; set; } = string.Empty;

        public int? Rating { get; set; }

        public DateTime Date { get; set; }

        public List<string> UserImg { get; set; } = new List<string>();

        public int? HotelId { get; set; }

        public Hotel? Hotel { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}