using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Dtos
{
    public class UpdatePasswordDTO
    {
        public string Token { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;
    }
}