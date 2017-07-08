using _01.GringottsDatabase.Models;

namespace _01.GringottsDatabase
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GringottsContext : DbContext
    {
        public GringottsContext()
            : base("name=GringottsContext")
        {
        }

        public virtual DbSet<WizardDeposits> WizardDeposits { get; set; }
    }
}