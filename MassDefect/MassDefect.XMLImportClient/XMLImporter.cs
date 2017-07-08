namespace MassDefect.XMLImportClient
{
    using System.Xml.Linq;
    using System;
    using System.Linq;
    using System.Xml.XPath;
    using Data;
    using Models;

    public class XMLImporter
    {
        private const string NewAnomaliesPath = "../../../datasets/new-anomalies.xml";
        private const string InvalidDataMessage = "Error: Invalid data.";

        public static void Main()
        {
            var xml = XDocument.Load(NewAnomaliesPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");

            var context = new MassDefectContext();
            foreach (var anomaly in anomalies)
            {
                ImportAnomalyAndVictims(anomaly, context);
            }
        }

        #region ImportAnomalyAndVictims
        private static void ImportAnomalyAndVictims(XElement anomalyNode, MassDefectContext context)
        {
            var originPlanetName = anomalyNode.Attribute("origin-planet");
            var teleportPlanetName = anomalyNode.Attribute("teleport-planet");
            if (originPlanetName == null || teleportPlanetName == null)
            {
                Console.WriteLine(InvalidDataMessage);

                return;
            }

            var anomalyEntity = new Anomalie()
            {
                OriginPlanet = GetPlanetByName(originPlanetName.Value, context),
                TeleportPlanet = GetPlanetByName(teleportPlanetName.Value, context)
            };

            if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
            {
                Console.WriteLine(InvalidDataMessage);
                return;
            }
            
            context.Anomalies.Add(anomalyEntity);
            Console.WriteLine($"Successfully imported anomaly.");

            var victims = anomalyNode.XPathSelectElements("victims/victim");
            foreach (var victim in victims)
            {
                ImportVictim(victim, context, anomalyEntity);
            }

            context.SaveChanges();

        }

        #endregion

        #region ImportVictim

        private static void ImportVictim(XElement victim, MassDefectContext context, Anomalie anomaly)
        {
            var name = victim.Attribute("name");
            if (name == null)
            {
                return;
            }

            var personEntity = GetPersonByName(name.Value, context);
            if (personEntity == null)
            {
                Console.WriteLine(InvalidDataMessage);

                return;
            }

            anomaly.Victims.Add(personEntity);
            Console.WriteLine($"Successfully imported Anomaly Victim {name.Value}.");
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
    }
}