public class ConsoleView
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
}