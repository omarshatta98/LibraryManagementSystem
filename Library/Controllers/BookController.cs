using Library.Models;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        readonly IBookService _bookService;
        readonly IUserService _userService;

        public BookController()
        {
            _bookService = new BookService();
            _userService = new UserService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Assign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _bookService.GetBookById(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            var IsAvailableToBorrow = _bookService.IsAvailableToBorrow(id.Value);
            if (IsAvailableToBorrow)
            {
                BorrowBooks borrowBook = new BorrowBooks { BookId = book.Id };
                // Get all users that not assign to this book.
                ViewBag.UserId = new SelectList(_userService.GetUsersNotAssignToBook(book.Id), "Id", "Name");
                return View(borrowBook);
            }
            else
            {
                ViewBag.msg = "sorry no book available to borrow";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(BorrowBooks borrowBook)
        {
            if (ModelState.IsValid)
            {
                _bookService.AssignBookToUser(borrowBook);
                return RedirectToAction("index");
            }
            return View(borrowBook);
        }
        [HttpGet]
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _bookService.GetBookById(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            var IsBorrowed = _bookService.IsBorrowed(id.Value);
            if (IsBorrowed)
            {
                BorrowBooks borrowBook = new BorrowBooks { BookId = book.Id };
                // Users that borrow this book only
                ViewBag.UserId = new SelectList(_userService.GetBorrowersOfBook(book.Id), "Id", "Name");
                return View(borrowBook);
            }
            else
            {
                ViewBag.msg = "sorry no book available to return";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int bookId, int userId)
        {
            _bookService.DeleteBookFromUser(bookId, userId);
            return RedirectToAction("Return");
        }
    }
}