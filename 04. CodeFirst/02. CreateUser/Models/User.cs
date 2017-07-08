namespace _02.CreateUser.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;
    using System.IO;
    using Attributes;
    using System.Collections.Generic;


    public class User
    {
        public User()
        {
            this.Friends = new HashSet<User>();
            this.Albums = new HashSet<Album>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(4), MaxLength(30), Required]
        public string Username { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Password(6, 50, ShouldContainDigit = true, ShouldContainLowercase = true, ShouldContainSpecialSymbol = true, ShouldContainUppercase = true)]
        public string Password { get; set; }

        [Email]
        public string Email { get; set; }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public Town BirthTown { get; set; }

        public Town CurrentTown { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        [NotMapped]
        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}
