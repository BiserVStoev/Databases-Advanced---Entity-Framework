namespace _02.GetVillains_Names
{
    using System;
    using System.Data.SqlClient;

    public class GetVillainsNames
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string selectionCommand = "SELECT v.Name, COUNT(*) FROM Villains AS v " +
                                      "INNER JOIN MinionsVillains AS m " +
                                      "ON v.VillainID = m.VillainID " +
                                      "GROUP BY v.Name " +
                                      "HAVING COUNT(*) > 3 " +
                                      "ORDER BY COUNT(*) DESC";

            SqlCommand command = new SqlCommand(selectionCommand, connection);
            SqlDataReader reader = command.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[0] + " " + reader[1]);
                }
            }
            connection.Close();
        }
    }
}
