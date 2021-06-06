using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace BookApp
{

    public class GlobalMediaTypeFormatters
    {

        /* This class is used to format the output response and enables developers to add different formats in a central place for easy maintainability */

        public JsonMediaTypeFormatter JsonFormatter
        {
            get
            {
                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;

                json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ContractResolver = new CamelCasePropertyNamesContractResolver();
                json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                return formatter;
            }
        }
    }
}