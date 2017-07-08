namespace MassDefect.Data
{
    using System.Data.Entity;
    using Models;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public IDbSet<Anomalie> Anomalies { get; set; }
        
        public IDbSet<Person> People { get; set; }
        
        public IDbSet<Planet> Planets { get; set; }
        
        public IDbSet<SolarSystem> SolarSystems { get; set; } 

        public IDbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomalie>()
                .HasMany<Person>(anomalie => anomalie.Victims)
                .WithMany(person => person.Anomalies)
                .Map(configuration =>
                {
                    configuration.MapLeftKey("AnomalyId");
                    configuration.MapRightKey("PersonId");
                    configuration.ToTable("AnomalyVictims");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}