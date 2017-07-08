namespace _01.InitialSetup
{
    using System.Data.SqlClient;
    using System;

    public class InitialSetup
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=master; Trusted_Connection=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string createDataBaseCommand = "CREATE DATABASE Minions";

            string useMinions = "USE Minions ";

            string createTableTownsCommand = "CREATE TABLE Towns "
                                             + "("
                                             + "TownID INT PRIMARY KEY IDENTITY, "
                                             + "TownName Varchar(50) NOT NULL, "
                                             + "Country VARCHAR(50) "
                                             + ") ";

            string createTableMinionsCommand = "CREATE TABLE Minions "
                                               + "("
                                               + "MinionID INT PRIMARY KEY IDENTITY, "
                                               + "Name Varchar(50) NOT NULL, "
                                               + "Age INT, "
                                               + "TownID INT, "
                                               + "FOREIGN KEY(TownID) REFERENCES Towns(TownID) "
                                               + ") ";

            string createTableVillainsCommand = "CREATE TABLE Villains "
                                                + "("
                                                + "VillainID INT PRIMARY KEY IDENTITY, "
                                                + "Name Varchar(50) NOT NULL, "
                                                + "Age INT, "
                                                + "EvilnessFactor VARCHAR(50) "
                                                + ") ";

            string createTableMinionsVilions = "CREATE TABLE MinionsVillains " +
                                               "(" + "MinionID INT, "
                                               + "VillainID INT, "
                                               + "PRIMARY KEY(MinionID, VillainID), "
                                               + "FOREIGN KEY(MinionID) REFERENCES Minions(MinionID), "
                                               + "FOREIGN KEY(VillainID) REFERENCES Villains(VillainID) "
                                               + ") ";

            string insertIntoTowns = "INSERT INTO Towns (TownName, Country) "
                                     + "VALUES "
                                     + "('Sofia', 'Bulgaria' ), "
                                     + "('London', 'England' ), "
                                     + "('Berlin', 'Germany'), "
                                     + "('Plovdiv', 'Bulgaria' ), "
                                     + "('Burgas', 'Bulgaria' )";

            string insertIntoMinions = "INSERT INTO Minions (Name, Age, TownID) "
                                       + "VALUES "
                                       + "('Bob', 10, 1 ), "
                                       + "('Kevin', 2, 2 ), "
                                       + "('Stuart', 7, 3), "
                                       + "('Siu', 4, 4 ), "
                                       + "('Pepi', 8, 5 )";

            string insertIntoVillians = "INSERT INTO Villains (Name, Age, EvilnessFactor) "
                                        + "VALUES "
                                        + "('Bobi', 10, 'good' ), "
                                        + "('Kev', 2, 'good' ), "
                                        + "('Student', 7, 'bad'), "
                                        + "('Sius', 4, 'bad' ), "
                                        + "('Pepina', 8, 'evil' ) ";

            string insertMinionsVillansCommandString = "INSERT INTO MinionsVillains (MinionID, VillainID) "
                                                       + "VALUES "
                                                       + "(1, 1), (2, 1), (3, 1), (4, 5), (5, 4) ";

            SqlCommand command = new SqlCommand(createDataBaseCommand, connection);
            using (connection)
            {
                try
                {
                    ExecuteCommand(command, useMinions);
                    ExecuteCommand(command, createTableTownsCommand);
                    ExecuteCommand(command, createTableMinionsCommand);
                    ExecuteCommand(command, createTableVillainsCommand);
                    ExecuteCommand(command, createTableMinionsVilions);
                    ExecuteCommand(command, insertIntoTowns);
                    ExecuteCommand(command, insertIntoMinions);
                    ExecuteCommand(command, insertIntoVillians);
                    ExecuteCommand(command, insertMinionsVillansCommandString);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close(); //IS THIS EVEN NECESSARY WHEN WE HAVE 'USING'???
                }
            }
        }

        private static void ExecuteCommand(SqlCommand command, string commandText)
        {
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }
    }
}
