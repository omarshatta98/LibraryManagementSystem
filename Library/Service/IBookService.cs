using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void CreateBook(Book book);
        void UpdateBook(Book book);
        bool IsAvailableToBorrow(int id);
        bool IsBorrowed(int id);
        void AssignBookToUser(BorrowBooks borrowBook);
        void DeleteBookFromUser(int bookId, int userId);
    }
}