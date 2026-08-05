using Microsoft.Data.Sqlite;

var repository = new TicketRepository();
repository.InitializeDatabase();

string titleVariable = " ";
string descriptionVariable = " ";
string statusVariable = " ";

string idVariable = " ";
string newStatus = " ";


string valg = " ";

while(valg != "0")
{
    Console.WriteLine("Hvilken oppgave skal du utføre:\n 1 for å opprette en ny sak \n 2 for å oppdatere en sak \n 3 for å se alle saker \n 4 for å slette saker \n 0 for å avslutte programmet");
    valg = Console.ReadLine();

    switch(valg)
    {
        case "1":
        {
            Console.WriteLine("Skriv inn Titlen til saken: ");
            titleVariable = Console.ReadLine();

            Console.WriteLine("Skriv inn Beskrivelse til saken: ");
            descriptionVariable = Console.ReadLine();

            Console.WriteLine("Skriv inn Status til saken(Skriv 0 for nye saker): ");
            statusVariable = Console.ReadLine();
            if (int.TryParse(statusVariable, out int statusNr))
            {
                repository.AddTicket(titleVariable, descriptionVariable, statusNr);  
            }
            else
            {
                Console.WriteLine("Ugyldig inndata");
            }

            break;
        }
    
        case "2":
        {
            Console.WriteLine("Hva er ID-en til saken du vil redigere");
            idVariable = Console.ReadLine();
            
            Console.WriteLine("hva er den nye statusen?: ");
            newStatus = Console.ReadLine();
            if (int.TryParse(newStatus, out int statusNr) && int.TryParse(idVariable, out int IdNr))
            {
                repository.UpdateStatus(IdNr, statusNr);
            }
            else
            {
                Console.WriteLine("Ugyldig inndata");
            }

            

            break;
        }

        case "3":
        {    
            var tickets = repository.GetAllTickets();
            foreach (var ticket in tickets)
                {
                    Console.WriteLine($"Id: {ticket.Id}, Title: {ticket.Title}, Description: {ticket.Description}, Status: {ticket.Status}");
                }
            break;
        }

        case "4":
        {
            Console.WriteLine("Hva er ID-en til saken du vil slette");
            idVariable = Console.ReadLine();

            if (int.TryParse(idVariable, out int IdNr))
            {
                repository.DeleteTicket(IdNr);
            }
            else
            {
                Console.WriteLine("Ugyldig inndata");
            }
            

            break;
        }
        default:
        Console.WriteLine("Ugylidig valg");
        break;
    }
}