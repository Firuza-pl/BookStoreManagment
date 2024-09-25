namespace Library.Application.ViewModel.Books;
public class GetRecordDTO
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
}
