using System;

namespace Command.Util.DataTypes
{
  public static class UtilEnum
  {
    public static bool TryParse<T>(object value, out T enumValue)
    {
      enumValue = default(T);

      try
      {
        enumValue = UtilEnum.Parse<T>(value);
      }
      catch
      {
        return false;
      }

      return true;
    }

    public static T Parse<T>(object value) => (T)Enum.Parse(
      typeof(T),
      Enum.GetName(typeof(T), Convert.ToChar(value)),
      ignoreCase: true
    );
  }
}
