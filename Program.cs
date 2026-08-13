using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;

var repository = new TicketRepository();
repository.InitializeDatabase();

var consoleView = new ConsoleView();

var controller  =new TicketController(repository, consoleView);
controller.Run();