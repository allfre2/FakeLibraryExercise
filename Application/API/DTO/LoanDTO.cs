namespace Application.DTO;

public class LoanDTO
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public int BookId { get; set; }
}
