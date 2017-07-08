namespace _06.RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    public class RemoveVillain
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                string getVillainDataCommand = "SELECT VillainID, " +
                                               "Name " +
                                               "FROM villains " +
                                               "WHERE VillainID = @villainId";
                SqlCommand villains = new SqlCommand(getVillainDataCommand, connection);
                villains.Parameters.AddWithValue("@villainId", villainId);

                string deleteVillainCommand = "DELETE FROM Villains " +
                                              "WHERE VillainID = @villainId";
                SqlCommand deleteVillain = new SqlCommand(deleteVillainCommand, connection);
                deleteVillain.Parameters.AddWithValue("@villainId", villainId);

                string freeMinionsCommand = "DELETE FROM MinionsVillains " +
                                            "WHERE VillainID = @villainId";
                SqlCommand freeMinions = new SqlCommand(freeMinionsCommand, connection);
                freeMinions.Parameters.AddWithValue("@villainId", villainId);

                villains.Transaction = transaction;
                SqlDataReader reader = villains.ExecuteReader();
                try
                {
                    reader.Read();
                    var villainName = (string) reader["Name"];
                    reader.Close();

                    freeMinions.Transaction = transaction;
                    var freedMinions = freeMinions.ExecuteNonQuery();

                    deleteVillain.Transaction = transaction;
                    deleteVillain.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine($"{villainName} was deleted");
                    Console.WriteLine($"{freedMinions} were released");
                }
                catch (InvalidOperationException e)
                {
                    reader.Close();
                    transaction.Rollback();
                    Console.WriteLine("No such villain was found");
                }
                
            }
        }
    }
}
