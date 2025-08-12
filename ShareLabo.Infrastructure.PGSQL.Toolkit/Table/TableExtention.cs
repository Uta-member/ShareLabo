using EFCore.NamingConventions.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Reflection;

namespace ShareLabo.Infrastructure.PGSQL.Toolkit
{
    public static class TableExtention
    {
        public static Dictionary<string, object?> ToSnakeCaseDictionary<T>(this T obj)
        {
            var dictionary = new Dictionary<string, object?>();
            foreach(var property in typeof(T).GetProperties())
            {
                var dbGeneratedAttribute = property.GetCustomAttribute<DatabaseGeneratedAttribute>();
                if(dbGeneratedAttribute != null &&
                    dbGeneratedAttribute.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
                {
                    continue;
                }
                var snakeCaseNameRewriter = new SnakeCaseNameRewriter(CultureInfo.CurrentCulture);
                var snakeCaseName = snakeCaseNameRewriter.RewriteName(property.Name);
                var value = property.GetValue(obj);
                dictionary.Add(snakeCaseName, value);
            }
            return dictionary;
        }
    }
}