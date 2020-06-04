using System;
using System.IO;
using Newtonsoft.Json;

namespace Inha.Commons.Log.Utils
{
    public class HttpPostedFileConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stream = (Stream)value;
            if (stream != null)
            {
                writer.WriteValue(string.Empty);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Stream));
        }
    }
}
