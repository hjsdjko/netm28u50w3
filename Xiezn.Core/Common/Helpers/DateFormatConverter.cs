using Newtonsoft.Json;
using System;

namespace Xiezn.Core.Common.Helpers
{
    public class DateFormatConverter : JsonConverter
    {
        private readonly string _dateFormat;

        public DateFormatConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try {
                return DateTime.Parse(reader.Value.ToString());
            }
            catch {
                return DateTime.Parse(DateTime.Now.ToString());
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(_dateFormat));
        }
    }

}
