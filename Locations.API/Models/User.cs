using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string ip_address { get; set; }
        [Required]
        public double latitude { get; set; }
        [Required]
        public double longitude { get; set; }
        [Required]
        public string city { get; set; }

        [Required]
        public string Country { get; set; }

        public string ZipCode { get; set; }
        public string Area { get; set; }
        public double DistanceFromLondon { get; set; }

    }
}
