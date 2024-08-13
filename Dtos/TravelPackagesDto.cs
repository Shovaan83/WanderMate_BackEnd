using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Dtos
{
    public class TravelPackagesDto
    {
        public string? Name {get; set;}

        public string? Description {get; set;}

        public int? Price {get; set;}

        public string? ImageUrl {get; set;}

        public string? Location {get; set;}
    }
}