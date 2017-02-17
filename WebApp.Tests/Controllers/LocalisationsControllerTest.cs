using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Tests.Controllers
{
    [TestFixture]
    public class LocalisationsControllerTests
    {
        private Mock<DataLayer> dataLayerMock;
        private LocalisationsController controller;
        private List<Localisation> localisations;
        private Localisation localisation;

        [SetUp]
        public void Setup()
        {
            this.localisation = new Localisation
            {
                Key = "TestKey",
                Language = "en",
                Value = "Hello World"
            };

            this.localisations = new List<Localisation> { this.localisation };

            this.dataLayerMock = new Mock<DataLayer>(new DataDbContext(new DbContextOptions<DataDbContext>()));
            this.dataLayerMock.Setup(x => x.GetLocalisations()).Returns(this.localisations);
            this.dataLayerMock.Setup(x => x.GetLocalisations(It.IsAny<string>())).Returns(this.localisations);
            this.dataLayerMock.Setup(x => x.GetLocalisationsAsJson()).Returns(this.CreateJson(this.localisations));
            this.dataLayerMock.Setup(x => x.GetLocalisationsAsJson(It.IsAny<string>())).Returns(this.CreateJson(this.localisations));
            this.controller = new LocalisationsController(this.dataLayerMock.Object);
        }

        private string CreateLocalizationKey(Localisation localization)
        {
            var key = $"{localization.Key}";
            return key;
        }

        private JObject CreateJson(List<Localisation> localisations)
        {
            var jObject = new JObject();
            localisations.ForEach(x => jObject[this.CreateLocalizationKey(x)] = x.Value);
            return jObject;
        }

        [Test]
        public void GetLocalisations_WithoutParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisations();
            var dataValue = (List<Localisation>)((OkObjectResult)data).Value;
            Assert.That(data, Is.TypeOf<OkObjectResult>());
            Assert.That(dataValue.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetLocalisations_WithParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisations("en");
            var dataValue = (List<Localisation>)((OkObjectResult)data).Value;
            Assert.That(data, Is.TypeOf<OkObjectResult>());
            Assert.That(dataValue.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetLocalisationsAsJson_WithoutParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisationsAsJson();
            var dataValue = (JObject)((OkObjectResult)data).Value;
            Assert.That(data, Is.TypeOf<OkObjectResult>());
            Assert.That(dataValue.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetLocalisationsAsJson_WithParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisationsAsJson("en");
            var dataValue = (JObject)((OkObjectResult)data).Value;
            Assert.That(data, Is.TypeOf<OkObjectResult>());
            Assert.That(dataValue.Count, Is.EqualTo(1));
        }

        [Test]
        public void UpdateLocalisation_OkObjectResult()
        {
            var data = this.controller.UpdateLocalisation(this.localisation);
            Assert.That(data, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void AddLocalisation_OkObjectResult()
        {
            var data = this.controller.AddLocalisation(this.localisation);
            Assert.That(data, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void RemoveLocalisation_OkObjectResult()
        {
            var data = this.controller.AddLocalisation(this.localisation);
            Assert.That(data, Is.TypeOf<OkObjectResult>());
        }
    }
}
