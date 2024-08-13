using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Models
{
    public class Destination
    {
        [Key]

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Weather {get; set;} = string.Empty;

        public List<string> ImageUrl {get; set;} = new List<string>();

        public string Description {get; set;} = string.Empty;
    }
}