public interface ITicketRepository
{

    void InitializeDatabase();
    void AddTicket(string title, string description, TicketStatus status);
    bool UpdateStatus(int id, TicketStatus newStatus);
    List<Ticket> GetAllTickets();
    bool DeleteTicket(int id);

}