namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }

    public int AuthorId { get; set; }
    public virtual Author Author { get; set; }

    public virtual ICollection<Loan> Loans { get; set; }
}
