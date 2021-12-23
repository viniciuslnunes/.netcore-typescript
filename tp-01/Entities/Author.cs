using Command.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Command.Entities
{
  public class Author
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public char Gender { get; set; }

    public override string ToString() => this.ToolerString();
  }
}