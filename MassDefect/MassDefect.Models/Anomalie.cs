namespace MassDefect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Anomalie
    {
        public Anomalie()
        {
            this.Victims = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }
        
        public Planet OriginPlanet { get; set; }
        
        public Planet TeleportPlanet { get; set; }

        [Required]
        public virtual ICollection<Person> Victims { get; set; } 
    }
}
