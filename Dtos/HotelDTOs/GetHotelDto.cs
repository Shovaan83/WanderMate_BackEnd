using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Dtos.HotelDto
{
    public class GetHotelDto
    {
        public int Id { get; set; }

        public string? Name {get; set;}
        public string? Description {get; set; } = String.Empty;
        public List<string> ImageUrl = new List<string>();
        public int? Price { get; set; } 

        public bool FreeCancellation {get; set;}

        public bool ReserveNow {get; set;}
}
}