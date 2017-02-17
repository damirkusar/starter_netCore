using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Tests.Integration.Controllers
{
    [TestFixture]
    public class LocalisationsControllerIntegrationTest
    {
        private LocalisationsController controller;

        [SetUp]
        public void Setup()
        {
            this.controller = new LocalisationsController(DbContextFactory.DataAccessLayerInstance);
        }

        [Test]
        public void GetLocalisations_DE_OkObjectResult()
        {
            var localisations = this.controller.GetLocalisations("DE");
            Assert.That(localisations, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetLocalisations_DE_Has_Localisations()
        {
            var localisations = (List<Localisation>)((OkObjectResult)this.controller.GetLocalisations("DE")).Value;
            Assert.That(localisations.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetLocalisationsAsJson_DE_Has_Localisations()
        {
            var localisations = (JObject)((OkObjectResult)this.controller.GetLocalisationsAsJson("DE")).Value;
            Assert.That(localisations.Count, Is.EqualTo(0));
        }
    }
}
