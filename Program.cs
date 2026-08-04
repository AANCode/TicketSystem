using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;


using var connection = new SqliteConnection("Data Source=support.db");
connection.Open();


string sql = @"
    CREATE TABLE IF NOT EXISTS Tickets (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Title TEXT NOT NULL,
        Description TEXT,
        Status INTEGER NOT NULL
    );
";

using var command = new SqliteCommand(sql, connection);
command.ExecuteNonQuery();

Console.WriteLine("Database og tabell er opprettet");


string titleVariable = " ";
string descriptionVariable = " ";
string statusVariable = " ";

string idVariable = " ";
string nyStatus = " ";


string valg = " ";

while(valg != "0")
{
    Console.WriteLine("Hvilken oppgave skal du utføre:\n 1 for å opprette en ny sak \n 2 for å oppdatere en sak \n 3 for å se alle saker \n 0 for å avslutte programmet");
    valg = Console.ReadLine();

    if (valg == "1")
    {
        Console.WriteLine("Skriv inn Titlen til saken: ");
        titleVariable = Console.ReadLine();

        Console.WriteLine("Skriv inn Beskrivelse til saken: ");
        descriptionVariable = Console.ReadLine();

        Console.WriteLine("Skriv inn Status til saken(Skriv 0 for nye saker): ");
        statusVariable = Console.ReadLine();


        string insertsql = @"
        INSERT INTO Tickets (Title, Description, Status)
        VALUES (@title, @description, @status);
        ";

        using var insertcommand = new SqliteCommand(insertsql, connection);
        insertcommand.Parameters.AddWithValue("@title", titleVariable);
        insertcommand.Parameters.AddWithValue("@description", descriptionVariable);
        insertcommand.Parameters.AddWithValue("@status", statusVariable);
        insertcommand.ExecuteNonQuery();

        Console.WriteLine("Saken har blitt lagt inn i Databasen");
    }
    else if(valg == "2")
    {
        Console.WriteLine("Hva er ID-en til saken du vil redigere");
        idVariable = Console.ReadLine();
        
        Console.WriteLine("hva er den nye statusen?: ");
        nyStatus = Console.ReadLine();


        string updatesql = @"
        UPDATE Tickets SET Status = @status WHERE Id = @id;
        ";

        using var updateCommand = new SqliteCommand(updatesql, connection);
        updateCommand.Parameters.AddWithValue("@status", nyStatus);
        updateCommand.Parameters.AddWithValue("@id", idVariable);
        updateCommand.ExecuteNonQuery();
    }
    else if(valg == "3")
    {
        string readersql =@"
            SELECT* FROM Tickets;
        ";

        using var selectCommand = new SqliteCommand(readersql, connection);
        using var reader = selectCommand.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Title: {reader["Title"]}, Description: {reader["Description"]}, Status: {reader["Status"]}");
        }
    }
    else
    {
        Console.WriteLine("Ugylidig valg");
    }
}