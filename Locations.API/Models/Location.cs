﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Created { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Stablished { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
