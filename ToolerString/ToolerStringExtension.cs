using System;
using System.Collections;

namespace ToolerString
{
  public static class ToolerStringExtention
  {
    /// <summary>
    /// It formats and returns the current object in an exotic string format designed by Wellington Tuler
    /// </summary>

    public static string ToolerString(this object value)
    {
      var stringifiedObject = $"{value.GetType().Name}[";

      var properties = value.GetType().GetProperties();

      foreach (var property in properties)
      {
        var formattedValue = "";

        if (
          typeof(IEnumerable).IsAssignableFrom(property.PropertyType)
          && !typeof(String).IsAssignableFrom(property.PropertyType)
        )
        {
          formattedValue += $"{property.Name}={{";

          foreach (var item in (ICollection)property.GetValue(value))
            formattedValue += $"{item.ToString()},";

          formattedValue = formattedValue.Substring(0, formattedValue.Length - 1) + "}";
        }
        else
        {
          var isNull = String.IsNullOrEmpty(property.GetValue(value)?.ToString());

          var propertyValue = isNull
            ? null
            : property.GetValue(value).ToString();

          formattedValue = $"{property.Name}={propertyValue ?? "?"}";
        }

        stringifiedObject += $"{formattedValue},";
      }

      stringifiedObject = stringifiedObject.Substring(0, stringifiedObject.Length - 1) + "]";

      return stringifiedObject;
    }
  }
}
