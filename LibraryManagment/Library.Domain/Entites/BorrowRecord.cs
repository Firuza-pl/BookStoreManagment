namespace Library.Domain.Entites;
public class BorrowRecord
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    public bool IsReturned { get; set; }
    // Navigation properties
    public Book? Book { get; set; }
    public Member? Member { get; set; }
}