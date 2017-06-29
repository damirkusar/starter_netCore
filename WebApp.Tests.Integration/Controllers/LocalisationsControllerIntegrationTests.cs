using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApp.Controllers;
using WebApp.DataAccessLayer.Views;
using Xunit;

namespace WebApp.Tests.Integration.Controllers
{
    public class LocalisationsControllerIntegrationTest
    {
        private LocalisationsController controller;

        public LocalisationsControllerIntegrationTest()
        {
            this.controller = new LocalisationsController(DbContextFactory.DataAccessLayerInstance);
        }

        [Fact]
        public void GetLocalisations_DE_OkObjectResult()
        {
            var localisations = this.controller.GetLocalisations("DE");
            Assert.IsType<OkObjectResult>(localisations);
        }

        [Fact]
        public void GetLocalisations_DE_Has_Localisations()
        {
            var localisations = (List<Localisation>)((OkObjectResult)this.controller.GetLocalisations("DE")).Value;
            Assert.Equal(0, localisations.Count);
        }

        [Fact]
        public void GetLocalisationsAsJson_DE_Has_Localisations()
        {
            var localisations = (JObject)((OkObjectResult)this.controller.GetLocalisationsAsJson("DE")).Value;
            Assert.Equal(0, localisations.Count);
        }
    }
}
