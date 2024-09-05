namespace Library.Domain.Entites;
 public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsAvailable { get; set; }
    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }
}
