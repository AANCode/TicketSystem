public interface IView
{
    string GetMainMenuChoice();
    string GetTicketTitle();
    string GetTicketDescription();
    string GetTicketId();
    void ShowTickets(List<Ticket> tickets);



    void ShowSuccessMessage(string successMessage);
    void ShowErrorMessage(string ErrorMessage);


    TicketStatus GetValidStatusFromUser();
}