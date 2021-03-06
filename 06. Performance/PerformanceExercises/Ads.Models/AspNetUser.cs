namespace Ads.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AspNetUser
    {

        public AspNetUser()
        {
            Ads = new HashSet<Ad>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? TownId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public virtual ICollection<Ad> Ads { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
