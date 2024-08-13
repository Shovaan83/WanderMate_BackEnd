using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Models
{
    public class TopDestinations
    {
        [Key]

        public int Id { get; set; }

        public string Name {get; set;} = string.Empty;

        public int? Price {get; set;} 

        public List<string> ImageUrl {get; set;} = new List<string>();

        public string Description {get; set;} = string.Empty;
    }
}