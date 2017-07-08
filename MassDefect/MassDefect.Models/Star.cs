﻿namespace MassDefect.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Star
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [Required]
        public virtual SolarSystem SolarSystem { get; set; }
    }
}
