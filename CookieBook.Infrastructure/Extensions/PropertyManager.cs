using System.Reflection;

namespace CookieBook.Infrastructure.Extensions
{
    public static class PropertyManager
    {
        public static bool PropertiesExists<T>(string properties)
        {
            var splitedProperties = properties.Split(',');

            var exists = true;

            foreach (var property in splitedProperties)
            {
                var propertyName = property.Trim();

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