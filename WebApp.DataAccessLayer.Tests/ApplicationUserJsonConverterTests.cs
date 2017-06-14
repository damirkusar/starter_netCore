using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NuGet.Protocol.Core.v3;
using WebApp.DataAccessLayer.Models;
using Xunit;

namespace WebApp.DataAccessLayer.Tests
{
    public class ApplicationUserJsonConverterTests
    {
        private ApplicationUser applicationUser;

        public ApplicationUserJsonConverterTests()
        {
            this.applicationUser = new ApplicationUser
            {
                FirstName = "Damir",
                LastName = "Kusar",
                AssignedRoles = new List<string>() { "Admin" }
            };
        }

        [Fact]
        public void WriteJson()
        {
            var expected =
                "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"FirstName\":\"Damir\",\"LastName\":\"Kusar\",\"UserName\":null,\"Email\":null,\"Image\":null,\"AssignedRoles\":[\"Admin\"]}";
            var userJson = applicationUser.ToJson();
            Assert.Equal(expected.Length, userJson.Length);
        }

        [Fact]
        public void ReadJson()
        {
            var userObject = new ApplicationUser();
            Assert.Equal(null, userObject.AssignedRoles);

            var userJson = applicationUser.ToJson();
            JsonConvert.PopulateObject(userJson, userObject);
            Assert.Equal("Admin", userObject.AssignedRoles.ToList()[0]);
        }
    }
}
