using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace NCDCWebService.Converters
{
    public class NCDCDateConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
    {
        /// <summary>
        /// The date pattern for most dates returned by the API
        /// </summary>
        protected const string DateFormat = "yyyy-MM-dd%K";
        protected const string DateFormatV2 = "yyyy-MM-ddThh:mm:ss.FFF%K";

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>The parsed value as a DateTime, or null.</returns>
        /// <remarks></remarks>
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.Value == null || reader.Value.GetType() != typeof(string))
                return new DateTime();

            DateTime parsedDate;

            try
            {
                return DateTime.ParseExact(
                    (string)reader.Value,
                    DateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
            catch
            {
                return DateTime.TryParseExact(
                    (string)reader.Value,
                    DateFormatV2,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate) ? parsedDate : new DateTime();

            }
        }

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value.GetType() != typeof(DateTime))
                throw new ArgumentOutOfRangeException("value", "The value provided was not the expected data type.");

            writer.WriteValue(((DateTime)value).ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
