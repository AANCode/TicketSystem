
List<Ticket> tickets = new List<Ticket>();

Ticket ticket1 = new Ticket
{
    Id = 1,
    Title = "mus",
    Description = "Jeg trenger en ny mus",
    Status = 0 // Open
};

tickets.Add(ticket1);

foreach (var ticket in tickets)
{
    Console.WriteLine($"Ticket ID: {ticket.Id}, Title: {ticket.Title}");
}


foreach (var ticket in tickets)
{
    if (ticket.Id == 1)
    {
        ticket.Status = 1; // Update status to In Progress
        Console.WriteLine($"Ticket ID: {ticket.Id} status updated to In Progress.");
    }
}

for (int i = 0; i < tickets.Count; i++)
{
    if (tickets[i].Id == 1)
    {
        tickets.RemoveAt(i);
        Console.WriteLine("Ticket has been removed.");
    }
}