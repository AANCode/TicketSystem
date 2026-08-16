public interface IView
{
    string GetMainMenuChoice();
    int GetValidTicketId();
    string GetTicketTitle();
    string GetTicketDescription();
    
    void ShowTickets(List<Ticket> tickets);
    void ShowTicket(Ticket ticket);
    TicketStatus GetValidStatusFromUser();


    void ShowSuccessMessage(string successMessage);
    void ShowErrorMessage(string ErrorMessage);


    
}