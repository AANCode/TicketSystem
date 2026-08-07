using Microsoft.Data.Sqlite;

var repository = new TicketRepository();
repository.InitializeDatabase();


string? valg = "";

while(valg != "0")
{
    Console.WriteLine("Hvilken oppgave skal du utføre:\n 1 for å opprette en ny sak \n 2 for å oppdatere en sak \n 3 for å se alle saker \n 4 for å slette saker \n 0 for å avslutte programmet");
    valg = Console.ReadLine();

    switch(valg)
    {
        case "1":
        {
            HandleCreateTicket();
            break;
        }
    
        case "2":
        {
            HandleUpdateTicket();
            break;
        }

        case "3":
        {    
            HandleShowAllTickets();
            break;
        }

        case "4":
        {
            HandleDeleteTicket();
            break;
        }
        default:
        Console.WriteLine("Ugylidig valg");
        break;
    }




    void PrintStatusMenu()
    {
        foreach (var status in Enum.GetValues<TicketStatus>())
        {
            Console.WriteLine ($"{(int)status} = {status}");
        }
    }
    
    TicketStatus GetValidStatusFromUser()
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

    void HandleCreateTicket()
    {
        Console.WriteLine("Skriv inn Titlen til saken: ");
        string? titleVariable = Console.ReadLine();

        Console.WriteLine("Skriv inn Beskrivelse til saken: ");
        string? descriptionVariable = Console.ReadLine();

            
        TicketStatus valgstatus = GetValidStatusFromUser();
        repository.AddTicket(titleVariable, descriptionVariable, valgstatus);
    }

    void HandleUpdateTicket()
    {
        Console.WriteLine("Hva er ID-en til saken du vil redigere");
        string? idVariable = Console.ReadLine();


        if (int.TryParse(idVariable, out int IdNr))
        {
            TicketStatus valgstatus = GetValidStatusFromUser();
            repository.UpdateStatus(IdNr, valgstatus);
        }
        else
        {
            Console.WriteLine("Ugyldig Id.");
        } 
    }

    void HandleShowAllTickets()
    {
        var tickets = repository.GetAllTickets();
        foreach (var ticket in tickets)
        {
            Console.WriteLine($"Id: {ticket.Id}, Title: {ticket.Title}, Description: {ticket.Description}, Status: {ticket.Status}");
        }
    }

    void HandleDeleteTicket()
    {
        
        Console.WriteLine("Hva er ID-en til saken du vil slette");
        string? idVariable = Console.ReadLine();

        if (int.TryParse(idVariable, out int IdNr))
        {
            repository.DeleteTicket(IdNr);
        }
        else
        {
            Console.WriteLine("Ugyldig inndata");
        }
    }
}