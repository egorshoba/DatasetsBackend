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
                {
                    var boolParsed = bool.TryParse(value.ToString(), out bool boolParsedValue);

                    if (boolParsed)
                        castedValue = boolParsedValue;
                    else
                        throw new ArgumentException($"{property.Name} is in wrong format");
                }

                if (property.PropertyType == typeof(DateTime))
                {
                    var dateParsed = DateTime.TryParse(value.ToString(), out DateTime parsedDateValue);

                    if (dateParsed)
                        castedValue = parsedDateValue;
                    else
                        throw new ArgumentException($"{property.Name} is in wrong format");
                }

                if (property.PropertyType == typeof(AnswersLocation))
                {
                    var enumParsed = Enum.TryParse(value.ToString(), true, out AnswersLocation enumParsedValue);

                    if (enumParsed)
                        castedValue = enumParsedValue;

                    else
                        throw new ArgumentException($"{property.Name} is in wrong format");
                }

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
