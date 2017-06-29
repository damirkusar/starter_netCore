using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;
using WebApp.Controllers;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Views;
using Xunit;

namespace WebApp.Tests.Controllers
{
    public class LocalisationsControllerTests
    {
        private Mock<DataLayer> dataLayerMock;
        private LocalisationsController controller;
        private List<Localisation> localisations;
        private Localisation localisation;

        public LocalisationsControllerTests()
        {
            this.localisation = new Localisation
            {
                Key = "TestKey",
                LanguageIsoAlpha2 = "en",
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

        [Fact]
        public void GetLocalisations_WithoutParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisations();
            var dataValue = (List<Localisation>)((OkObjectResult)data).Value;
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(1, dataValue.Count);
        }

        [Fact]
        public void GetLocalisations_WithParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisations("en");
            var dataValue = (List<Localisation>)((OkObjectResult)data).Value;
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(1, dataValue.Count);
        }

        [Fact]
        public void GetLocalisationsAsJson_WithoutParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisationsAsJson();
            var dataValue = (JObject)((OkObjectResult)data).Value;
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(1, dataValue.Count);
        }

        [Fact]
        public void GetLocalisationsAsJson_WithParameter_OkObjectResult_CountIsCorrect()
        {
            var data = this.controller.GetLocalisationsAsJson("en");
            var dataValue = (JObject)((OkObjectResult)data).Value;
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(1, dataValue.Count);
        }

        [Fact]
        public void UpdateLocalisation_OkObjectResult()
        {
            var data = this.controller.UpdateLocalisation(this.localisation);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void AddLocalisation_OkObjectResult()
        {
            var data = this.controller.AddLocalisation(this.localisation);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void RemoveLocalisation_OkObjectResult()
        {
            var data = this.controller.AddLocalisation(this.localisation);
            Assert.IsType<OkObjectResult>(data);
        }
    }
}
