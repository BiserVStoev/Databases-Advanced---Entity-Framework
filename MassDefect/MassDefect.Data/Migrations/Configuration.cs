namespace MassDefect.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MassDefect.Data.MassDefectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MassDefect.Data.MassDefectContext context)
        {
        }
    }
}
