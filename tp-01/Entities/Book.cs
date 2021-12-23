using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Command.Extensions;

namespace Command.Entities
{
  public class Book
  {
    public Book()
    {
      this.Authors = new List<Author>();
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public IList<Author> Authors { get; set; }
    public decimal Price { get; set; }
    public long Quantity { get; set; }

    public string GetAuthorNames() => string.Join(
      ",",
      this.Authors.Select(p => p.Name)
    );

    public override string ToString() => this.ToolerString();
  }
}