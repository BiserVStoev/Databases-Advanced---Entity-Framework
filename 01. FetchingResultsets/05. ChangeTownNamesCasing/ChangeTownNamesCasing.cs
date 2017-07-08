namespace _05.ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    public class ChangeTownNamesCasing
    {
        public static void Main()
        {
            string country = Console.ReadLine();
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectTown = "SELECT TownName FROM Towns " +
                                    "WHERE Country = @countryName";
                SqlCommand townSelectionCommand = new SqlCommand(selectTown, connection);
                townSelectionCommand.Parameters.AddWithValue("@countryName", country);

                SqlDataReader townsReader = townSelectionCommand.ExecuteReader();
                List<string> towns = new List<string>();
                while (townsReader.Read())
                {
                    towns.Add((string)townsReader[0]);
                }

                townsReader.Close();

                List<string> townsChanged = new List<string>();
                foreach (string town in towns)
                {
                    if (town != town.ToUpper())
                    {
                        townsChanged.Add(town.ToUpper());
                        string updateTowns = "UPDATE Towns " +
                                             "SET TownName = @upperName " +
                                             "WHERE TownName = @townName";
                        SqlCommand updateCommand = new SqlCommand(updateTowns, connection);
                        updateCommand.Parameters.AddWithValue("@upperName", town.ToUpper());
                        updateCommand.Parameters.AddWithValue("@townName", town);
                        updateCommand.ExecuteNonQuery();
                    }
                }

                StringBuilder townsRow = new StringBuilder();
                if (townsChanged.Count != 0)
                {
                    townsRow.AppendLine($"{townsChanged.Count} towns were affected.");
                    townsRow.AppendLine($"[{string.Join(", ", townsChanged)}]");
                }
                else
                {
                    townsRow.AppendLine("No town names were affected.");
                }

                Console.WriteLine(townsRow.ToString());
            }
        }
    }
}
