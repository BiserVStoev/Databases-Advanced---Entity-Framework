namespace _08.IncreaseMinionsAge
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class IncreaseMinionsAge
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int[] villainIdsData = Console.ReadLine().Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                
                string updateCommandString = GetBuildCommand(villainIdsData, connection);

                SqlCommand updateCommand = new SqlCommand(updateCommandString, connection);
                for (int i = 0; i < villainIdsData.Length; i++)
                {
                    updateCommand.Parameters.AddWithValue(@"minionId" + i, villainIdsData[i]);
                }

                updateCommand.ExecuteNonQuery();

                string selectAllFromMinions = "SELECT * FROM Minions";
                SqlCommand selectCommand = new SqlCommand(selectAllFromMinions, connection);
                SqlDataReader minionsReader = selectCommand.ExecuteReader();
                while (minionsReader.Read())
                {
                    for (int i = 0; i < minionsReader.FieldCount; i++)
                    {
                        Console.Write($"{minionsReader[i]} ");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static string GetBuildCommand(int[] villainIdsData, SqlConnection connection)
        {
            StringBuilder commandBuilder = new StringBuilder();
            for (int i = 0; i < villainIdsData.Length; i++)
            {
                string currentMinionName = GetMinionName(villainIdsData[i], connection);
                if (currentMinionName.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Length > 1)
                {
                    string[] uppedNames = currentMinionName.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < uppedNames.Length; j++)
                    {
                        uppedNames[j] = uppedNames[j].Substring(0, 1).ToUpper() + uppedNames[j].Substring(1, uppedNames[j].Length - 1);
                    }

                    currentMinionName = string.Join(" ", uppedNames);
                    commandBuilder.AppendLine("UPDATE Minions " +
                                          $"SET Age = Age + 1, Name = '{currentMinionName}' " +
                                          "WHERE MinionID = @minionId" + i);
                }
                else
                {
                    commandBuilder.AppendLine("UPDATE Minions " +
                                          "SET Age = Age + 1, Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)) " +
                                          "WHERE MinionID = @minionId" + i);
                }
                
                commandBuilder.AppendLine();
            }

            string command = commandBuilder.ToString();

            return command;
        }

        private static string GetMinionName(int minionID, SqlConnection connection)
        {
            string commandText = "SELECT Name " +
                                "FROM Minions " +
                                "WHERE MinionID = @minionId";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@minionId", minionID);

            string minionName = (string)command.ExecuteScalar();

            return minionName;
        }
    }
}
