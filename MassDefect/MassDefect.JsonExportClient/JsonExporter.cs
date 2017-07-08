namespace MassDefect.JsonExportClient
{
    using System.IO;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;

    public class JsonExporter
    {
        public static void Main()
        {
            var context = new MassDefectContext();

            //ExportPlanetsWhichAreNotAnomalyOrigins(context);
            //ExportPeopleWhichHaveNotBeenVictims(context);
            ExportTopAnomaly(context);


        }

    #region ExportTopAnomaly
        private static void ExportTopAnomaly(MassDefectContext context)
        {
            //var exportedTopAnomaly = context.Anomalies.
        }
        #endregion

    #region ExportPeopleWhichHaveNotVeenVictims
        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectContext context)
        {
            var exportedPeople = context.People
                .Where(p => p.Anomalies.Count == 0)
                .Select(p => new 
                {
                    name = p.Name,
                    homePlanet = new
                    {
                        name = p.HomePlanet.Name
                    }
                });

            var personsAsJason = JsonConvert.SerializeObject(exportedPeople, Formatting.Indented);
            File.WriteAllText("../../persons.json", personsAsJason);
        }
        #endregion

    #region ExportPlanetsWhichAreNotAnomalyOrigins
        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(planet => !planet.OriginAnomalies.Any())
                .Select(planet => new
                {
                    name = planet.Name
                });

            var planetAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText("../../planets.json", planetAsJson);
        }    
    }
    #endregion
}