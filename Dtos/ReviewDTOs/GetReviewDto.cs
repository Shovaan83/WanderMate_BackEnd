using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeb.Dtos.ReviewDTOs
{
    public class GetReviewDto
    {
        public int? Id { get; set; }

        public string? ReviewText {get; set;}

        public int? Rating { get; set; }

    }
}