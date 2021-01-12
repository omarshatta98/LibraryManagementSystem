using Library.Data.Repositories;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IBookService _bookService;

        public UserService()
        {
            _userRepository = new Repository<User>();
            _bookService = new BookService();
        }

        public void CreateUser(User user)
        {
            _userRepository.Add((user));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }
        public IEnumerable<User> GetUsersNotAssignToBook(int bookId)
        {
            var assignUsers = GetBorrowersOfBook(bookId);
            return GetAllUsers().Where(u => assignUsers.All(a => a.Id != u.Id));
        }
        public IEnumerable<User> GetBorrowersOfBook(int bookId)
        {
            var book = _bookService.GetBookById(bookId);
            return book.BorrowBooks.Select(u => u.User).ToList();
        }
    }
}