using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Locations.API.Models.Trail;

namespace Locations.API.Models.DTOs
{
    public class TrailUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }

        [Required]
        public double Elevation { get; set; }
        public DificultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        //public NationalParkDto NationalPark { get; set; }

    }
}
