using Library.Domain.Entites.BookAggregate;
using Library.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories;
public class BookRepository : GenericRepository<Book>, IBookRepository
{
}
