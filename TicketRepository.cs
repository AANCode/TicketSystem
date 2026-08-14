using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

public class TicketRepository : ITicketRepository
{
    private readonly string _connectionString = "Data Source=support.db";
    private SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        return connection;
    }


    public void InitializeDatabase()
    {
        using SqliteConnection connection = GetConnection();


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
    }


    public void AddTicket(string title, string description, TicketStatus status)
    {
        using SqliteConnection connection = GetConnection();

        string insertsql = @"
            INSERT INTO Tickets (Title, Description, Status)
            VALUES (@title, @description, @status);
        ";

        using var insertcommand = new SqliteCommand(insertsql, connection);
        insertcommand.Parameters.AddWithValue("@title", title);
        insertcommand.Parameters.AddWithValue("@description", description);
        insertcommand.Parameters.AddWithValue("@status", status);
        insertcommand.ExecuteNonQuery();
    }


    public bool UpdateStatus(int id, TicketStatus newStatus)
    {
        using SqliteConnection connection = GetConnection();

        string updatesql = @"
            UPDATE Tickets SET Status = @status WHERE Id = @id;
            ";

        using var updateCommand = new SqliteCommand(updatesql, connection);
        updateCommand.Parameters.AddWithValue("@status", newStatus);
        updateCommand.Parameters.AddWithValue("@id", id);
        int rowsAffected = updateCommand.ExecuteNonQuery();

        return rowsAffected > 0;
    }


    public List<Ticket> GetAllTickets()
    {

        using SqliteConnection connection = GetConnection();

        string readersql = @"
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
                Status = (TicketStatus)Convert.ToInt32(reader["Status"])
            };

            tickets.Add(ticket);
        }

        return tickets;
    }


    public Ticket? GetTicketById(int id)
    {
        using SqliteConnection connection = GetConnection();

        string readersql = @"
            SELECT * FROM Tickets WHERE Id = @id;
            ";
        
        using var singelSelectCommand = new SqliteCommand (readersql, connection);
        singelSelectCommand.Parameters.AddWithValue("@id", id);
        using var reader = singelSelectCommand.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        var ticket = new Ticket
        {
            Id = Convert.ToInt32(reader["Id"]),
            Title = reader["Title"].ToString(),
            Description = reader["Description"].ToString(),
            Status = (TicketStatus)Convert.ToInt32(reader["Status"])
        };

        return ticket;
    }


    public bool DeleteTicket(int id)
    {
        using SqliteConnection connection = GetConnection();

        string deletesql = @"
                DELETE FROM Tickets WHERE Id = @id;
            ";

        using var deleteCommand = new SqliteCommand(deletesql, connection);
        deleteCommand.Parameters.AddWithValue("@id", id);
        int rowsAffected = deleteCommand.ExecuteNonQuery();

        return rowsAffected > 0;
    }


}