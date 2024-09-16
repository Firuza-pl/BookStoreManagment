﻿using Library.Shared.Kernel.Core;
using Library.SharedKernel.Domain.Enums;

namespace Library.Domain.Entites.BookAggregate;
public class Book : Entity, IAggregateRoot
{
    public string? Title { get; private set; }
    public BookStatus IsAvailable { get; private set; }
    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }
    public Book(string? title, BookStatus isAvailable = BookStatus.Active) => (Title, IsAvailable) = (title, isAvailable); //add
    public void Edit(string? title)
    {
        Title = title;
        IsAvailable = BookStatus.Active;
    }

    public void Delete()
    {
        IsAvailable = BookStatus.Deleted;
    }

    //MUST TO DO: BorrowBook() also raise an event : AddDomainEvent
}
