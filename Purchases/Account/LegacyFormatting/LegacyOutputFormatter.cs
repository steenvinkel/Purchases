using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Buffers;

namespace Account.LegacyFormatting
{
    public class LegacyOutputFormatter : JsonOutputFormatter
    {
        public LegacyOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool) : base(serializerSettings, charPool)
        {
            serializerSettings.Converters.Add(new LegacyJsonConverter());
        }
    }
}
