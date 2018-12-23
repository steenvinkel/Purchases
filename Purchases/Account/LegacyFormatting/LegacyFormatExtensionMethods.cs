using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Buffers;
using System.IO;

namespace Account.LegacyFormatting
{
    public static class LegacyFormatExtensionMethods
    {
        public static ObjectResult EnableLegacyFormatting(this ObjectResult result, HttpRequest req)
        {
            if (ShouldBeLegacyFormat(req))
            {
                AddLegacyFormatting(result);
            }

            return result;
        }

        private static bool ShouldBeLegacyFormat(HttpRequest req)
        {
            string name = req.Query["formatting"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name == "legacy";
        }

        private static void AddLegacyFormatting(ObjectResult objectResult)
        {
            objectResult.Formatters.Add(CreateLegacyFormatter());
        }

        private static IOutputFormatter CreateLegacyFormatter()
        {
            var serializerSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            return new LegacyOutputFormatter(serializerSettings, ArrayPool<char>.Shared);
        }
    }
}
