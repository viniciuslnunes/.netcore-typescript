using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Command.Controllers
{
  public abstract class BaseController
  {
    private readonly string _controllerName = "Book";
    protected readonly JsonSerializerSettings _jsonSerializerSettings;
    protected HttpContext _httpContext;

    public BaseController(string controllerName)
    {
      this._controllerName = controllerName;

      this._jsonSerializerSettings = new JsonSerializerSettings()
      {
        Formatting = Formatting.None,
        ContractResolver = new CamelCasePropertyNamesContractResolver()
      };
    }

    public void setHttpContext(HttpContext httpContext)
    {
      this._httpContext = httpContext;
    }

    protected Task Ok(object response)
    {
      var serializedResponse = JsonConvert.SerializeObject(
        response,
        this._jsonSerializerSettings
      );

      return this._httpContext.Response.WriteAsync(serializedResponse);
    }

    protected Task View(string viewName, Dictionary<string, string> data)
    {
      var viewsPath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "Views"
      );

      var foundPaths = Directory
        .GetDirectories(viewsPath)
        .Select(p =>
          p.Split('/').LastOrDefault()
        );

      if (!foundPaths.Contains(this._controllerName))
        throw new Exception(
          $"No valid directory was found for controller \"{this._controllerName}\"."
        );

      var controllerViewsPath = Path.Combine(viewsPath, this._controllerName);

      var availableViews = Directory
        .GetFiles(controllerViewsPath)
        .Select(p =>
          p.Split('/').LastOrDefault()
        );

      var validViewName = availableViews.FirstOrDefault(p =>
        p.Contains($"{viewName}.")
      );

      if (validViewName is null)
        throw new Exception(
          $"No view named \"{viewName}\" was found for controller \"{this._controllerName}\"."
        );

      this._httpContext.Response.ContentType = "text/html";

      var html = File.ReadAllText(Path.Combine(
        controllerViewsPath,
        validViewName
      ));

      data.Keys.ToList().ForEach(key =>
        html = html.Replace($"{{{{{key}}}}}", data[key])
      );

      return this._httpContext.Response.WriteAsync(html);
    }
  }
}