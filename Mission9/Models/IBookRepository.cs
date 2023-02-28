using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Models
{
    // implement the repository to connect to the db
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
    }
}
