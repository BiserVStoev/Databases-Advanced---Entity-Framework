namespace _09.IncreaseAgeStoredProcedure
{
    using System;
    using System.Data.SqlClient;

    public class IncreaseAgeStoredProcedure
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int minionId = int.Parse(Console.ReadLine());
                string update = "EXEC usp_GetOlder @minionId";

                connection.Open();
                SqlCommand updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@minionId", minionId);
                updateCommand.ExecuteNonQuery();

                string select = "SELECT NAME, Age FROM Minions " +
                                "WHERE MinionId = @minionId";
                SqlCommand selectGivenMinionCommand = new SqlCommand(select, connection);
                selectGivenMinionCommand.Parameters.AddWithValue("@minionId", minionId);
                SqlDataReader minionData = selectGivenMinionCommand.ExecuteReader();
                while (minionData.Read())
                {
                    string minionName = minionData["Name"].ToString();
                    int age = (int) minionData["Age"];
                    Console.WriteLine(minionName + " " + age);
                }
            }
        }
    }
}
