public class ConsoleView : IView
{
    public string GetMainMenuChoice()
    {
        Console.WriteLine("Hvilken oppgave skal du utføre:\n 1 for å opprette en ny sak \n 2 for å oppdatere en sak \n 3 for å se alle saker \n 4 for å slette saker \n 0 for å avslutte programmet");
        string valg = Console.ReadLine();
        return valg;
    }

    public string GetTicketTitle()
    {
        Console.WriteLine("Skriv inn Titlen til saken: ");
        string title = Console.ReadLine();
        return title;
    }

    public string GetTicketDescription()
    {
        Console.WriteLine("Skriv inn Beskrivelse til saken: ");
        string description = Console.ReadLine();
        return description;
    }

    public string GetTicketId()
    {
        Console.WriteLine("Skriv inn Id til saken: ");
        string id = Console.ReadLine();
        return id;
    }

    public void ShowTickets(List<Ticket> tickets)
    {
        foreach (var ticket in tickets)
        {
            Console.WriteLine($"Id: {ticket.Id}, Title: {ticket.Title}, Description: {ticket.Description}, Status: {ticket.Status}");
        }
    }



    public void ShowSuccessMessage(string successMessage)
    {
        Console.WriteLine($"{successMessage}");
    }

    public void ShowErrorMessage(string successMessage)
    {
        Console.WriteLine($"{successMessage}");
    }



    void PrintStatusMenu()
    {
        foreach (var status in Enum.GetValues<TicketStatus>())
        {
            Console.WriteLine($"{(int)status} = {status}");
        }
    }

    public TicketStatus GetValidStatusFromUser()
    {
        while (true)
        {
            Console.WriteLine($"Velg status:");
            PrintStatusMenu();
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int statusNr) && Enum.IsDefined(typeof(TicketStatus), statusNr))
            {
                return (TicketStatus)statusNr;
            }
            else
            {
                Console.WriteLine("Ugyldig status, Du må velge et av de gyldige altenativene");
            }
        }

    }


}