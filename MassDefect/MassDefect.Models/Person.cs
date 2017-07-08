namespace MassDefect.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Person
    {
        public Person()
        {
            this.Anomalies = new HashSet<Anomalie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Planet HomePlanet { get; set; }

        [Required]
        public virtual ICollection<Anomalie> Anomalies { get; set; }
    }
}
