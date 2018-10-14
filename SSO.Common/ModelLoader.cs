using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace SSO.Common
{
    public static class ModelLoader
    {
        /// <summary>
        /// Generic function that will take data reader and refactor the output object to match the reader columns and the object properties and populate the values to the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Load<T>(IDataReader reader) where T : new()
        {
            T result = new T();
            var objectProperties = result.GetType().GetProperties().Where(st => Attribute.IsDefined(st, typeof(ColumnAttribute))).ToList();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var readerColumnName = reader.GetName(i);
                var property = objectProperties.FirstOrDefault(st => st.Name == readerColumnName);
                if (property != null)
                {
                    property.SetValue(result, reader.GetValue(i));
                    objectProperties.Remove(property);
                }
            }

            return result;
        }
    }
}
