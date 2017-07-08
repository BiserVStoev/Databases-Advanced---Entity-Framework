using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.CreateUser.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Albums = new HashSet<Album>();
        }
        public int Id { get; set; }

        [Required]
        public string TagLabel { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
