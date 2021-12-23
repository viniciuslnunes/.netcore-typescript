using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Command.Entities;
using Command.Extensions;
using Newtonsoft.Json;

namespace Command.Controllers
{
  public class BookController : BaseController
  {
    public BookController() : base("Book") { }

    public IList<Book> Books => JsonConvert.DeserializeObject<IList<Book>>(
      File.ReadAllText(
        Path.Combine(Directory.GetCurrentDirectory(), "mock.json")
      )
    );

    private Book GetRandomBook()
    {
      var randomBook = Books[new Random().Next(0, Books.Count)];

      return randomBook;
    }

    public Task Get()
    {
      return Ok(Books);
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
