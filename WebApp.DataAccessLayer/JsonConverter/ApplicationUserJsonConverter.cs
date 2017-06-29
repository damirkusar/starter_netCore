using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApp.DataAccessLayer.Models;

namespace WebApp.DataAccessLayer.JsonConverter
{
    class ApplicationUserJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var user = value as ApplicationUser;
            //user.FullName = $"{user.FirstName} {user.LastName}";

            writer.WriteStartObject();

            if (user != null)
            {
                writer.WritePropertyName("id");
                writer.WriteValue(user.Id);
                writer.WritePropertyName("firstName");
                writer.WriteValue(user.FirstName);
                writer.WritePropertyName("lastName");
                writer.WriteValue(user.LastName);
                writer.WritePropertyName("fullName");
                writer.WriteValue(user.FullName);
                writer.WritePropertyName("userName");
                writer.WriteValue(user.UserName);
                writer.WritePropertyName("email");
                writer.WriteValue(user.Email);
                writer.WritePropertyName("image");
                writer.WriteValue(user.Image);
                writer.WritePropertyName("assignedRoles");

                writer.WriteStartArray();
                if (user.AssignedRoles != null)
                {
                    foreach (var userAssignedRole in user.AssignedRoles)
                    {
                        writer.WriteValue(userAssignedRole);
                    }
                }
                writer.WriteEndArray();
            }

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
