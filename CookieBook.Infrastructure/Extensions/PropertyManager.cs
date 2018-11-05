using System.Reflection;

namespace CookieBook.Infrastructure.Extensions
{
    public static class PropertyManager
    {
        public static bool PropertiesExists<T>(string[] properties)
        {
            var exists = true;

            foreach (var propertyName in properties)
            {
                var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                    BindingFlags.Instance | BindingFlags.Public);

                if (propertyInfo == null)
                {
                    exists = false;
                    break;
                }
            }

            return exists;
        }
    }
}