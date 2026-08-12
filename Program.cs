using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;

var repository = new TicketRepository();
var consoleView = new ConsoleView();
repository.InitializeDatabase();

string? valg = "";

while (valg != "0")
{
    valg = consoleView.GetMainMenuChoice();

    switch (valg)
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
        string? titleVariable = consoleView.GetTicketTitle();
        string? descriptionVariable = consoleView.GetTicketDescription();

        
        try
        {
        TicketStatus valgstatus = GetValidStatusFromUser();
        repository.AddTicket(titleVariable, descriptionVariable, valgstatus);
        Console.Write("Saken har blitt opprettet");
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"Det har skjedd en feil: {ex.Message}");
        }
    }


    void HandleUpdateTicket()
    {
        Console.WriteLine("Hva er ID-en til saken du vil redigere");
        string? idVariable = consoleView.GetTicketId();


        if (int.TryParse(idVariable, out int IdNr))
        {
            try
            {
                TicketStatus valgstatus = GetValidStatusFromUser();
                bool isUpdated = repository.UpdateStatus(IdNr, valgstatus);
                if (isUpdated)
                {
                    Console.WriteLine($"saken med id: {IdNr} har blitt oppdatert");  
                }
                else
                {
                    Console.WriteLine($"saken med ID: {IdNr} ble IKKE funnet");
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Det har skjedd en feil: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldig Id.");
        } 
    }


    void HandleShowAllTickets()
    {
        try
        {
            var tickets = repository.GetAllTickets();
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"Id: {ticket.Id}, Title: {ticket.Title}, Description: {ticket.Description}, Status: {ticket.Status}");
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"Det har skjedd en feil: {ex.Message}");
        }

        
    }


    void HandleDeleteTicket()
    {
        
        Console.WriteLine("Hva er ID-en til saken du vil slette");
        string? idVariable = Console.ReadLine();

        if (int.TryParse(idVariable, out int IdNr))
        {
            try
            {
                bool isDeleted = repository.DeleteTicket(IdNr);
                if (isDeleted)
                {
                    Console.WriteLine($"saken med ID: {IdNr} har blitt slettet");
                }
                else
                {
                    Console.WriteLine($"saken med ID: {IdNr} ble IKKE funnet");
                }
                
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Det har skjedd en feil: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ugyldig inndata");
        }
    }
}