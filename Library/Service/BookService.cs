using Library.Data.Repositories;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BorrowBooks> _borrowBookRepository;

        public BookService()
        {
            _bookRepository = new Repository<Book>();
            _borrowBookRepository = new Repository<BorrowBooks>();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll().ToList();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.Get(id);
        }
        public void CreateBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public bool IsAvailableToBorrow(int id)
        {
            var book = GetBookById(id);
            return book.BorrowedQuantity < book.Quantity;
        }

        public bool IsBorrowed(int id)
        {
            var book = GetBookById(id);
            return book.BorrowedQuantity > 0;
        }

        public void AssignBookToUser(BorrowBooks borrowBook)
        {
            var book = GetBookById(borrowBook.BookId);
            book.BorrowedQuantity++;

            _bookRepository.Update(book);
            _borrowBookRepository.Add(borrowBook);
        }

        public void DeleteBookFromUser(int bookId, int userId)
        {
            var borrowBook = _borrowBookRepository.Find(b => b.BookId == bookId & b.UserId == userId)
                .SingleOrDefault();
            var book = GetBookById(borrowBook.BookId);
            book.BorrowedQuantity--;

            _bookRepository.Update(book);
            _borrowBookRepository.Delete(borrowBook);
        }
    }
}