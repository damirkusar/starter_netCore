using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Core.v3;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer.JsonConverter
{
    class ApplicationUserJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var user = (value as ApplicationUser);
            user.ConcurrencyStamp = null;
            user.PasswordHash = null;
            user.SecurityStamp = null;

            writer.WriteStartObject();
            writer.WriteRaw(user.ToJson());
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var user = token.ToObject<ApplicationUser>();
            return user;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ApplicationUser);
        }
    }
}
