using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Command.Data;
using Command.Entities;
using Command.Extensions;
using MongoDB.Driver;

namespace Command.Controllers
{
  public class BookController : BaseController
  {
    public BookController() : base("Book") { }

    private Book GetRandomBook()
    {
      var mongoDbManager = new MongoDbManager();

      var bookCollection = mongoDbManager.Books;

      var books = bookCollection.Find(p => true).ToList();

      var randomBook = books[new Random().Next(0, books.Count)];

      return randomBook;
    }

    public Task Get()
    {
      var mongoDbManager = new MongoDbManager();

      var bookCollection = mongoDbManager.Books;

      var books = bookCollection.Find(p => true).ToList();

      return Ok(books);
    }

    public Task ShowBook()
    {
      var randomBook = this.GetRandomBook();

      var authorsList = string.Join(
        "",
        randomBook.Authors.Select(p =>
          $"<li>{p.Name}</li>"
        )
      );

      var data = new Dictionary<string, string>()
      {
        { "BookTitle", randomBook.Name },
        { "AuthorsList", authorsList },
      };

      return View("ShowBook", data);
    }

    public Task BookName()
    {
      var randomBook = this.GetRandomBook();

      var data = new Dictionary<string, string>()
      {
        { "BookTitle", randomBook.Name },
      };

      return View("BookName", data);
    }

    public Task ToolerString()
    {
      var randomBook = this.GetRandomBook();

      var data = new Dictionary<string, string>()
      {
        { "BookTitle", randomBook.Name },
        { "ToolerStringResult", randomBook.ToolerString() },
      };

      return View("ToolerString", data);
    }

    public Task GetAuthorsName()
    {
      var randomBook = this.GetRandomBook();

      var data = new Dictionary<string, string>()
      {
        { "BookTitle", randomBook.Name },
        { "GetAuthorsNameResult", randomBook.GetAuthorNames() },
      };

      return View("GetAuthorsName", data);
    }
  }
}