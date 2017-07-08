namespace MassDefect.XmlExportClient
{
    using System.Linq;
    using System.Xml.Linq;
    using Data;

    public class XmlExporter
    {
        public static void Main()
        {
            var context = new MassDefectContext();
            var exportedAnomalies = context.Anomalies
                .Select(anomaly => new
                {
                    id = anomaly.Id,
                    originPlanetName = anomaly.OriginPlanet.Name,
                    teleportPlanetName = anomaly.TeleportPlanet.Name,
                    victims = anomaly.Victims.Select(v => v.Name).ToList()
                });

            var orderedAnomalies = exportedAnomalies.OrderBy(a => a.id);

            var xmlDocument = new XElement("anomalies");

            foreach (var exportedAnomaly in orderedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", exportedAnomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", exportedAnomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleport-planet", exportedAnomaly.teleportPlanetName));

                foreach (var victim in exportedAnomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim));

                    anomalyNode.Add(victimNode);
                }

                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save("../../anomalies.xml");
        }
    }
}
