public class Ticket 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; } // 0 = Open, 1 = In Progress, 2 = Closed

}