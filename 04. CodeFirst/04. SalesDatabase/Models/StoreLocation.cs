namespace _04.SalesDatabase.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class StoreLocation
    {
        public StoreLocation()
        {
            this.Sales = new HashSet<Sale>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string LocationName { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
