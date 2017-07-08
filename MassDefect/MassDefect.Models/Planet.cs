using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefect.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public Planet()
        {
            this.OriginAnomalies = new HashSet<Anomalie>();
            this.TeleportAnomalies = new HashSet<Anomalie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Star Sun { get; set; }

        [Required]
        public virtual SolarSystem SolarSystem { get; set; }

        [InverseProperty("OriginPlanet")]
        public virtual ICollection<Anomalie> OriginAnomalies { get; set; } 

        [InverseProperty("TeleportPlanet")]
        public virtual ICollection<Anomalie> TeleportAnomalies { get; set; }
    }
}
