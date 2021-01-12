using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        IEnumerable<User> GetUsersNotAssignToBook(int bookId);
        IEnumerable<User> GetBorrowersOfBook(int bookId);
    }
}