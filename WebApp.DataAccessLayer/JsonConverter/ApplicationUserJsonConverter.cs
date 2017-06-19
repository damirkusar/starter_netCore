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
            var user = value as ApplicationUser;
            user.ConcurrencyStamp = string.Empty;
            user.PasswordHash = string.Empty;
            user.SecurityStamp = string.Empty;

            writer.WriteStartObject();

            writer.WritePropertyName("Id");
            writer.WriteValue(user.Id);
            writer.WritePropertyName("FirstName");
            writer.WriteValue(user.FirstName);
            writer.WritePropertyName("LastName");
            writer.WriteValue(user.LastName);
            writer.WritePropertyName("UserName");
            writer.WriteValue(user.UserName);
            writer.WritePropertyName("Email");
            writer.WriteValue(user.Email);
            writer.WritePropertyName("Image");
            writer.WriteValue(user.Image);
            writer.WritePropertyName("AssignedRoles");

            writer.WriteStartArray();
            if (user.AssignedRoles != null)
            {
                foreach (var userAssignedRole in user.AssignedRoles)
                {
                    writer.WriteValue(userAssignedRole);
                }
            }
            writer.WriteEndArray();

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
