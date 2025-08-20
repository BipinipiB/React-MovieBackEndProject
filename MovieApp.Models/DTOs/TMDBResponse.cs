using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieApp.Models.DTOs
{
    public class TMDBResponse
    {
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<TMDBMovieDto> Results { get; set; }
    }
}
