namespace Application.DTO;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Genre { get; set; }

    public int AuthorId { get; set; }
}
