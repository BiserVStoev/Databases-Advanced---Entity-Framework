﻿namespace _01.GringottsDatabase.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WizardDeposits
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(60), Required]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
        
        [Required]
        public int Age { get; set; }

        [MaxLength(100)]
        public string MagicWandCreator { get; set; }

        [Range(1, 32767)]
        public int MagicWandSize { get; set; }

        [MaxLength(20)]
        public string DepositGroup { get; set; }

        public DateTime? DepositStartDate { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DepositInterest { get; set; }

        public decimal DepositCharge { get; set; }

        public DateTime? DepositExpirationDate { get; set; }

        public bool IsDepositExpired { get; set; }
    }
}
