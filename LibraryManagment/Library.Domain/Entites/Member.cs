namespace Library.Domain.Entites;
public class Member
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }
}
