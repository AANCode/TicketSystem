using Microsoft.Data.Sqlite;

public class TicketController
{
    ITicketRepository repository;
    IView consoleView;

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
        string? titleVariable = consoleView.GetTicketTitle();
        string? descriptionVariable = consoleView.GetTicketDescription();


        try
        {
            TicketStatus valgstatus = consoleView.GetValidStatusFromUser();
            repository.AddTicket(titleVariable, descriptionVariable, valgstatus);
            consoleView.ShowSuccessMessage("Saken har blitt opprettet");
        }
        catch (SqliteException ex)
        {
            consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
        }
    }


    void HandleUpdateTicket()
    {
        string? idVariable = consoleView.GetTicketId();


        if (int.TryParse(idVariable, out int IdNr))
        {
            try
            {
                TicketStatus valgstatus = consoleView.GetValidStatusFromUser();
                bool isUpdated = repository.UpdateStatus(IdNr, valgstatus);
                if (isUpdated)
                {
                    consoleView.ShowSuccessMessage($"saken med id: {IdNr} har blitt oppdatert");
                }
                else
                {
                    consoleView.ShowErrorMessage($"saken med ID: {IdNr} ble IKKE funnet");
                }
            }
            catch (SqliteException ex)
            {
                consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
            }
        }
        else
        {
            consoleView.ShowErrorMessage("Ugyldig Id.");
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


    void HandleDeleteTicket()
    {
        string? idVariable = consoleView.GetTicketId();

        if (int.TryParse(idVariable, out int IdNr))
        {
            try
            {
                bool isDeleted = repository.DeleteTicket(IdNr);
                if (isDeleted)
                {
                    consoleView.ShowSuccessMessage($"saken med ID: {IdNr} har blitt slettet");
                }
                else
                {
                    consoleView.ShowErrorMessage($"saken med ID: {IdNr} ble IKKE funnet");
                }

            }
            catch (SqliteException ex)
            {
                consoleView.ShowErrorMessage($"Det har skjedd en feil: {ex.Message}");
            }
        }
        else
        {
            consoleView.ShowErrorMessage("Ugyldig inndata");
        }
    }
}