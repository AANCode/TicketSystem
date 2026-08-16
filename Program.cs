
ITicketRepository repository = new TicketRepository();
repository.InitializeDatabase();

IView consoleView = new ConsoleView();

var controller = new TicketController(repository, consoleView);
controller.Run();