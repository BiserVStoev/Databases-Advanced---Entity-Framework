namespace _04.AddMinion
{
    using System;
    using System.Data.SqlClient;

    public class AddMinion
    {
        public static void Main()
        {
            string[] minionData = Console.ReadLine().Split(':')[1].Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string villainName = Console.ReadLine().Split(':')[1].Trim();

            string minionName = minionData[0];
            int minionAge = int.Parse(minionData[1]);
            string minionTown = minionData[2];

            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                if (!CheckIfTownExists(minionTown, connection))
                {
                    AddTown(minionTown, connection);

                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                if (!CheckIfVillainExists(villainName, connection))
                {
                    AddVillain(villainName, connection);

                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                int townId = GetTownIdByName(minionTown, connection);

                AddMinionToDB(minionName, minionAge, townId, connection);

                int minionId = GetMinionByName(minionName, connection);
                int villainId = GetVillainByName(villainName, connection);

                try
                {
                    MakeRelationBetweenVillainAndMinion(minionId, villainId, connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }

        }

        private static int GetTownIdByName(string minionTown, SqlConnection connection)
        {
            string commandText = "SELECT TownID " +
                                 "FROM Towns " +
                                 "WHERE TownName = @town";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@town", minionTown);

            int townId = (int)command.ExecuteScalar();

            return townId;
        }

        private static void MakeRelationBetweenVillainAndMinion(
            int minionId, 
            int villainId, 
            SqlConnection connection)
        {
            string commandText = "INSERT INTO MinionsVillains (MinionID, VillainID) " +
                                 "VALUES (@minionId, @villainID)";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@minionId", minionId);
            command.Parameters.AddWithValue("@villainID", villainId);
            command.ExecuteNonQuery();
        }

        private static int GetVillainByName(string villainName, SqlConnection connection)
        {
            string commandText = "SELECT VillainID " +
                                "FROM Villains " +
                                "WHERE Name = @name";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", villainName);

            int villainId = (int)command.ExecuteScalar();

            return villainId;
        }

        private static int GetMinionByName(string minionName, SqlConnection connection)
        {
            string commandText = "SELECT MinionID " +
                                "FROM Minions " +
                                "WHERE Name = @name";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", minionName);

            int villainId = (int)command.ExecuteScalar();

            return villainId;
        }

        private static void AddVillain(string villainName, SqlConnection connection)
        {
            string commandText = "INSERT INTO Villains (Name, EvilnessFactor) " +
                                 "VALUES (@villain, 'evil')";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@villain", villainName);
            command.ExecuteNonQuery();
        }

        private static bool CheckIfVillainExists(string villainName, SqlConnection connection)
        {
            string commandText = "SELECT COUNT (*) " +
                            "FROM Villains " +
                            "WHERE Name = @villainName";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@villainName", villainName);

            if ((int)command.ExecuteScalar() == 0)
            {
                return false;
            }

            return true;
        }

        private static void AddTown(string minionTown, SqlConnection connection)
        {
            string commandText = "INSERT INTO Towns (TownName) " +
                                 "VALUES (@town)";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@town", minionTown);
            command.ExecuteNonQuery();
        }

        private static bool CheckIfTownExists(string minionTown, SqlConnection connection)
        {
            string commandText = "SELECT COUNT (*) " +
                             "FROM Towns " +
                             "WHERE TownName = @townName";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@townName", minionTown);

            if ((int)command.ExecuteScalar() == 0)
            {
                return false;
            }

            return true;
        }

        private static void AddMinionToDB(string minionName, int minionAge, int townId, SqlConnection connection)
        {
            string commandText = "INSERT INTO Minions (Name, Age, TownID) " +
                                 "VALUES (@minion, @minionAge, @townId)";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@minion", minionName);
            command.Parameters.AddWithValue("@minionAge", minionAge);
            command.Parameters.AddWithValue("@townId", townId);
            command.ExecuteNonQuery();
        }
    }
}
