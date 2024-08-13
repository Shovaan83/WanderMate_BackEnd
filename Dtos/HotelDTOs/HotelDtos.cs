using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Dtos
{
    public class HotelDtos
    {
        public string? Name { get; set; }    

        public string? Description {get; set; } 

        public List<string> ImageUrls {get; set;} = new List<string>();

        public int? Price { get; set; } 

        public bool FreeCancellation {get; set;}

        public bool ReserveNow {get; set;}
    }
}