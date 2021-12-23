using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Command.Controllers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Command
{
  public class ApplicationRouter
  {
    private Dictionary<string, (BaseController controller, Func<Task> action)> _routes;

    public ApplicationRouter()
    {
      this._routes = new Dictionary<string, (BaseController controller, Func<Task> action)>();


      this.ConfigureRoutes();
    }

    private void ConfigureRoutes()
    {
      var bookController = new BookController();

      this.MapRoute("/books", bookController, bookController.Get);
      this.MapRoute("/books/book-name", bookController, bookController.BookName);
      this.MapRoute("/books/tooler-string", bookController, bookController.ToolerString);
      this.MapRoute("/books/authors-names", bookController, bookController.GetAuthorsName);
      this.MapRoute("/books/show-book", bookController, bookController.ShowBook);
    }

    public Task Router(HttpContext httpContext)
    {
      httpContext.Response.ContentType = "Application/json";

      var path = httpContext.Request.Path;

      if (this._routes.ContainsKey(path))
        return this.HandleMappedRouteInvocation(path, httpContext);

      return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
      {
        Message = "Route not found."
      }));
    }

    private Task HandleMappedRouteInvocation(
      string path,
      HttpContext httpContext
    )
    {
      try
      {
        (var controller, var action) = this._routes[path];

        controller.setHttpContext(httpContext);

        return action.Invoke();
      }
      catch (System.Exception exception)
      {
        var serializedResponse = JsonConvert.SerializeObject(exception);

        return httpContext.Response.WriteAsync(serializedResponse);
      }
    }

    public ApplicationRouter MapRoute(
      string path,
      BaseController controller,
      Func<Task> action
    )
    {
      this._routes.Add(
        path,
        (controller, action)
      );

      return this;
    }
  }
}