using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudCollector.Common
{
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        string str = "yyyy-MM-dd HH:mm:ss";
        public DatetimeJsonConverter(string TimeString)
        {
            str = TimeString;
        }
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(str));
        }
    }
}
