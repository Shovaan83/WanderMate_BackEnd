using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Dtos
{
    public class UserDto
    {
        public string? Email {get; set;}

        public string? Username {get; set;}

        public string? Role {get; set;}

        public string? Password {get; set;}

        public string? ConfrimPassword {get; set;}
    }
}