using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string ip_address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string city { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Area { get; set; }
        public double DistanceFromLondon { get; set; }
    }
}
