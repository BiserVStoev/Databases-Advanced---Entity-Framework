namespace _07.PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PrintAllMinionNames
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=Minions; Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectMinions = "SELECT Name FROM Minions";
                SqlCommand minionNames = new SqlCommand(selectMinions, connection);
                connection.Open();
                using (SqlDataReader reader = minionNames.ExecuteReader())
                {
                    List<string> names = new List<string>();
                    while (reader.Read())
                    {
                        names.Add(reader["Name"].ToString());
                    }

                    PrintNames(names);
                }
            }
        }

        private static void PrintNames(List<string> names)
        {
            int firstIndex = 0;
            int lastIndex = names.Count - 1;

            for (int i = 0; i < names.Count; i++)
            {
                int currentIndex;
                if (i % 2 == 0)
                {
                    currentIndex = firstIndex;
                    firstIndex++;
                }
                else
                {
                    currentIndex = lastIndex;
                    lastIndex--;
                }

                Console.WriteLine(names[currentIndex]);
            }
        }
    }
}
