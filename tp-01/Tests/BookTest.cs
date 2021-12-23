using System;
using Command.Entities;
using Newtonsoft.Json;

namespace Command.Tests
{
  public class BookTest
  {
    private bool _logSteps = false;
    public Book Book { get; set; }

    private BookTest SetLogSteps(bool value)
    {
      this._logSteps = value;

      return this;
    }

    public BookTest EnableStepsLogs() => this.SetLogSteps(true);
    public BookTest DisableStepsLogs() => this.SetLogSteps(false);

    public BookTest CreateBook()
    {
      if (this._logSteps)
        Console.WriteLine("Creating Book instance");

      this.Book = new Book();

      return this;
    }

    public BookTest DefineBookTitle(string title)
    {
      if (this._logSteps)
        Console.WriteLine("Defining Book title");

      this.Book.Name = title;

      return this;
    }

    public BookTest DefineBookPrice(decimal price)
    {
      if (this._logSteps)
        Console.WriteLine("Defining Book price");

      this.Book.Price = price;

      return this;
    }

    public BookTest DefineBookQuantity(long quantity)
    {
      if (this._logSteps)
        Console.WriteLine("Defining Book quantity");

      this.Book.Quantity = quantity;

      return this;
    }

    public BookTest AddAuthor(Author author)
    {
      if (this._logSteps)
        Console.WriteLine($"Adding author \"{author.Name}\" to Book.");

      this.Book.Authors.Add(author);

      return this;
    }

    public Book ExportBookCopy() => JsonConvert.DeserializeObject<Book>(
      JsonConvert.SerializeObject(this.Book)
    );
  }
}