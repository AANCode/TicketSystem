using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

public class TicketRepository
{
    private readonly string _connectionString = "Data Source=support.db";

    public void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
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
    }

    public void AddTicket(string title, string description, int status)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertsql = @"
            INSERT INTO Tickets (Title, Description, Status)
            VALUES (@title, @description, @status);
        ";

        using var insertcommand = new SqliteCommand(insertsql, connection);
        insertcommand.Parameters.AddWithValue("@title", title);
        insertcommand.Parameters.AddWithValue("@description", description);
        insertcommand.Parameters.AddWithValue("@status", status);
        insertcommand.ExecuteNonQuery();

        Console.WriteLine("Saken har blitt lagt inn i Databasen");
    }

    public void UpdateStatus(int id, int newStatus)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string updatesql = @"
            UPDATE Tickets SET Status = @status WHERE Id = @id;
            ";

            using var updateCommand = new SqliteCommand(updatesql, connection);
            updateCommand.Parameters.AddWithValue("@status", newStatus);
            updateCommand.Parameters.AddWithValue("@id", id);
            updateCommand.ExecuteNonQuery();
    }

    public List<Ticket> GetAllTickets()
    {
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string readersql =@"
            SELECT* FROM Tickets;
        ";

        using var selectCommand = new SqliteCommand(readersql, connection);
        using var reader = selectCommand.ExecuteReader();

        var tickets = new List<Ticket>();
        while (reader.Read())
        {
            var ticket = new Ticket
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString(),
                Status = Convert.ToInt32(reader["Status"])
            };

            tickets.Add(ticket);

            
        }

        return tickets;
    }

    public void DeleteTicket(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();


        string deletesql = @"
                DELETE FROM Tickets WHERE Id = @id;
            ";

            using var deleteCommand = new SqliteCommand(deletesql, connection);
            deleteCommand.Parameters.AddWithValue("@id", id);
            deleteCommand.ExecuteNonQuery();
    }

    
}