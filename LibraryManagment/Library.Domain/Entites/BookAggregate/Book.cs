﻿using Library.Domain.Core;

namespace Library.Domain.Entites.BookAggregate;
public class Book : Entity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsAvailable { get; set; }
    // Navigation property
    public ICollection<BorrowRecord>? BorrowRecords { get; set; }

    public Book(Guid id, string? title, bool isAvailable) => (Id, Title, IsAvailable) = (id, title, isAvailable);
}