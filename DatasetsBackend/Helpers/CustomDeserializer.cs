using DatasetsBackend.Data;
using System.Reflection;

namespace DatasetsBackend.Helpers
{
    public static class CustomDeserializer
    {
        public static T DeserializeForm<T>(IFormCollection keyValues) where T : new()
        {
            var properties = typeof(T).GetProperties();

            var result = new T();

            foreach (PropertyInfo property in typeof(T).GetProperties().Where(p => p.CanWrite))
            {
                var value = keyValues[property.Name];

                dynamic castedValue = "";

                if (property.PropertyType == typeof(string))
                    castedValue = (string)value;

                if (property.PropertyType == typeof(bool))
                    castedValue = bool.Parse(value.ToString());

                if (property.PropertyType == typeof(DateTime))
                    castedValue = DateTime.Parse(value.ToString());

                if (property.PropertyType == typeof(AnswersLocation))
                    castedValue = (AnswersLocation)Enum.Parse(typeof(AnswersLocation), value.ToString());

                try
                {
                    property.SetValue(result, castedValue, null);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Type cast is not implemented");
                    throw;
                }
            }

            return result;
        }

    }
}
