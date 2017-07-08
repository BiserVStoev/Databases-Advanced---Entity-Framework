namespace MassDefect.JsonImportClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Data;
    using Models;
    using Models.DTO;
    using Newtonsoft.Json;

    public class JsonImporter
    {
        private const string SolarSystemsPath = "../../../datasets/solar-systems.json";
        private const string StarsPath = "../../../datasets/stars.json";
        private const string PlanetsPath = "../../../datasets/planets.json";
        private const string PersonsPath = "../../../datasets/persons.json";
        private const string AnomaliesPath = "../../../datasets/anomalies.json";
        private const string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";
        private const string InvalidDataMessage = "Error: Invalid data.";

        public static void Main()
        {
            //ImportSolarSystems();
            //ImportStars();
            //ImportPlanets();
            //ImportPersons();
            //ImportAnomalies();
            //ImportAnomalyVictims();
        }

#region ImportAnomalyVictims
        private static void ImportAnomalyVictims()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(AnomalyVictimsPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(json);
            foreach (var anomalyVictim in anomalies)
            {
                if (anomalyVictim.Id == null || anomalyVictim.Person == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                var anomalyEntity = GetAnomalyById(anomalyVictim.Id, context);
                var personEntity = GetPersonByName(anomalyVictim.Person, context);

                if (anomalyEntity == null || personEntity == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                anomalyEntity.Victims.Add(personEntity);
                Console.WriteLine($"Successfully imported Anomaly Victim {anomalyVictim.Person}.");
            }

            context.SaveChanges();
        }
#endregion

#region GetPersonByName
        private static Person GetPersonByName(string person, MassDefectContext context)
        {
            var personEntity = context.People.FirstOrDefault(p => p.Name == person);
            if (personEntity == null)
            {
                return null;
            }

            return personEntity;
        }
#endregion

#region GetAnomalyById
        private static Anomalie GetAnomalyById(int? id, MassDefectContext context)
        {
            var anomalyEntity = context.Anomalies.FirstOrDefault(a => a.Id == id);
            if (anomalyEntity == null)
            {
                return null;
            }

            return anomalyEntity;
        }
#endregion

#region ImportAnomalies
        private static void ImportAnomalies()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(AnomaliesPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomaliesDTO>>(json);
            foreach (var anomaly in anomalies)
            {
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                var anomalyEntity = new Anomalie()
                {
                    OriginPlanet = GetPlanetByName(anomaly.OriginPlanet, context),
                    TeleportPlanet = GetPlanetByName(anomaly.TeleportPlanet, context)
                };

                if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                context.Anomalies.Add(anomalyEntity);
                Console.WriteLine($"Successfully imported Anomaly anomaly.");
            }

            context.SaveChanges();
        }
#endregion

#region ImportPersons
        private static void ImportPersons()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(PersonsPath);
            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);
            foreach (var person in persons)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                var personEntity = new Person()
                {
                    Name = person.Name,
                    HomePlanet = GetPlanetByName(person.HomePlanet, context)
                };

                if (personEntity.HomePlanet == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                context.People.Add(personEntity);
                Console.WriteLine($"Successfully imported Person {person.Name}.");
            }

            context.SaveChanges();
        }
#endregion

#region GetPlanetByName
        private static Planet GetPlanetByName(string name, MassDefectContext context)
        {
            var planetEntity = context.Planets.FirstOrDefault(p => p.Name == name);
            if (planetEntity == null)
            {
                return null;
            }

            return planetEntity;
        }
        #endregion

#region ImportPlanets
        private static void ImportPlanets()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(PlanetsPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);
            foreach (var planet in planets)
            {
                if (planet.Name == null || planet.SolarSystem == null || planet.SolarSystem == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                var planetEntity = new Planet()
                {
                    Name = planet.Name,
                    SolarSystem = GetSolarSystemByName(planet.SolarSystem, context),
                    Sun = GetStarByName(planet.Sun, context)
                };

                if (planetEntity.SolarSystem == null)
                {
                    Console.WriteLine(InvalidDataMessage);
                    continue;
                }

                if (planetEntity.Sun == null)
                {
                    Console.WriteLine(InvalidDataMessage);
                    continue;
                }

                context.Planets.Add(planetEntity);
                Console.WriteLine($"Successfully imported Star {planet.Name}.");
            }

            context.SaveChanges();
        }
        #endregion

#region GetStarByName
        private static Star GetStarByName(string sun, MassDefectContext context)
        {
            var sunEntity = context.Stars.FirstOrDefault(s => s.Name == sun);
            if (sunEntity == null)
            {
                return null;
            }

            return sunEntity;
        }
#endregion
        
#region ImportStars
        private static void ImportStars()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(StarsPath);
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);
            foreach (var star in stars)
            {
                if (star.Name == null || star.SolarSystem == null)
                {
                    Console.WriteLine(InvalidDataMessage);
                    
                    continue;
                }

                var starEntity = new Star()
                {
                    Name = star.Name,
                    SolarSystem = GetSolarSystemByName(star.SolarSystem, context)
                };

                if (starEntity.SolarSystem == null)
                {
                    Console.WriteLine(InvalidDataMessage);
                    continue;
                }

                context.Stars.Add(starEntity);
                Console.WriteLine($"Successfully imported Star {star.Name}.");
            }

            context.SaveChanges();
        }
        #endregion

#region GetSolarSystemByName
        private static SolarSystem GetSolarSystemByName(string solarSystem, MassDefectContext context)
        {
            var solarSystemEntity = context.SolarSystems.FirstOrDefault(s => s.Name == solarSystem);
            if (solarSystemEntity == null)
            {
                return null;
            }

            return solarSystemEntity;
        }
        #endregion

#region ImportSolarSystems
        private static void ImportSolarSystems()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(SolarSystemsPath);
            var solarySystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);
            foreach (var solarSystem in solarySystems)
            {
                if (solarSystem.Name == null)
                {
                    Console.WriteLine(InvalidDataMessage);

                    continue;
                }

                var solarSystemEntity = new SolarSystem()
                {
                    Name = solarSystem.Name
                };

                context.SolarSystems.Add(solarSystemEntity);
                Console.WriteLine($"Successfully imported Solar System {solarSystemEntity.Name}.");
            }

            context.SaveChanges();
        }
#endregion
    }
}