using Microsoft.Data.Sqlite;

public class TicketController
{
    private readonly ITicketRepository repository;
    private readonly IView consoleView;

    public TicketController(ITicketRepository repository, IView consoleView)
    {
        this.repository = repository;
        this.consoleView = consoleView;
    }

    public void Run()
    {
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

                case "5":
                    {
                        HandleShowTicketByID();
                        break;
                    }
                default:
                    {
                        if (valg == "0")
                        {
                            consoleView.ShowSuccessMessage("Applikasjon lukket");
                        }
                        else
                        {
                            consoleView.ShowErrorMessage($"Ugylidig valg");
                        }
                        break;
                    }
            }



        }
    }

    void HandleCreateTicket()
    {
        string? title = consoleView.GetTicketTitle();
        string? description = consoleView.GetTicketDescription();


        try
        {
            TicketStatus valgstatus = consoleView.GetValidStatusFromUser();
            repository.AddTicket(title, description, valgstatus);
            consoleView.ShowSuccessMessage("Saken har blitt opprettet");
        }
        catch (SqliteException ex)
        {
            consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
        }
    }


    void HandleUpdateTicket()
    {
        int id = consoleView.GetValidTicketId();

        try
        {
            TicketStatus valgstatus = consoleView.GetValidStatusFromUser();
            bool isUpdated = repository.UpdateStatus(id, valgstatus);
            if (isUpdated)
            {
                consoleView.ShowSuccessMessage($"saken med ID: {id} har blitt oppdatert");
            }
            else
            {
                consoleView.ShowErrorMessage($"saken med ID: {id} ble IKKE funnet");
            }
        }
        catch (SqliteException ex)
        {
            consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
        }
    }


    void HandleShowAllTickets()
    {
        try
        {
            var tickets = repository.GetAllTickets();
            consoleView.ShowTickets(tickets);
        }
        catch (SqliteException ex)
        {
            consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
        }
    }


    void HandleShowTicketByID()
    {
        int id = consoleView.GetValidTicketId();
        try
        {
            var ticket = repository.GetTicketById(id);
            if(ticket == null)
            {
                consoleView.ShowErrorMessage("Saken finnes ikke");
            }
            else
            {
                consoleView.ShowTicket(ticket);
            }
            
        }
        catch (SqliteException ex)
        {
           consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
        }
        
    }
    

    void HandleDeleteTicket()
    {
        int id = consoleView.GetValidTicketId();

        try
        {
            bool isDeleted = repository.DeleteTicket(id);
            if (isDeleted)
            {
                consoleView.ShowSuccessMessage($"saken med ID: {id} har blitt slettet");
            }
            else
            {
                consoleView.ShowErrorMessage($"saken med ID: {id} ble IKKE funnet");
            }

        }
        catch (SqliteException ex)
        {
            consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
        }
    }
}