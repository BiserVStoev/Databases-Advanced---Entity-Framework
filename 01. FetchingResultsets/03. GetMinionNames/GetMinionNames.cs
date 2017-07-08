namespace _03.GetMinionNames
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class GetMinionNames
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";

            SqlConnection connection = new SqlConnection(connectionString);
            int villainID = int.Parse(Console.ReadLine());
            string getVillainName = "SELECT Name FROM Villains WHERE VillainID = @villainId";

            SqlCommand command = new SqlCommand(getVillainName, connection);
            command.Parameters.AddWithValue("@villainId", villainID);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("No villain with ID " + villainID + " exists in the database.");
                    return;
                }

                reader.Read();
                Console.WriteLine("Villain: " + reader[0]);
            }
            
            connection.Close();

            string selectionCommand = "SELECT m.Name, m.Age " 
                                    + "FROM Villains AS v "
                                    + "INNER JOIN MinionsVillains AS mv "
                                    + "ON v.VillainID = mv.VillainID "
                                    + "INNER JOIN Minions AS m "
                                    + "ON mv.MinionID = m.MinionID "
                                    + "WHERE v.VillainID = @toBeReplaced";

            command = new SqlCommand(selectionCommand, connection);
            command.Parameters.AddWithValue("@toBeReplaced", villainID);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("<no minions>");
                    return;
                }

                int minionCounter = 1;

                while (reader.Read())
                {
                    Console.WriteLine($"{minionCounter}. {reader[0]} {reader[1]}");
                    minionCounter++;
                }
            }

            connection.Close();
        }
    }
}
