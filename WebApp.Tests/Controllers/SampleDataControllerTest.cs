using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.DataAccessLayer;
using WebApp.DataAccessLayer.Models;

namespace WebApp.Tests.Controllers
{
    [TestFixture]
    public class SampleDataControllerTest
    {
        private Mock<DataLayer> dataAccessLayerMock;
        private SampleDataController sampleDataController;

        [SetUp]
        public void Setup()
        {
            var localizations = new List<Localisation>
            {
                new Localisation
                {
                    LocalisationId = new Guid(),
                    Container = "TestContainer",
                    Key = "TestKey",
                    Language = "en",
                    Value = "Hello World"
                }
            };

            this.dataAccessLayerMock = new Mock<DataLayer>(new DataDbContext(new DbContextOptions<DataDbContext>()));
            this.dataAccessLayerMock.Setup(x => x.GetLocalisations()).Returns(localizations);
            this.sampleDataController = new SampleDataController(this.dataAccessLayerMock.Object);
        }
        
        [Test]
        public void GetSampleData_GetLocalisationDoesNotThrow_Ok()
        {
            var sampleData = this.sampleDataController.GetSampleData();
            Assert.That(sampleData, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetSampleData_Has_OneLocalization()
        {
            var sampleData = (IList<Localisation>)((OkObjectResult) this.sampleDataController.GetSampleData()).Value;
            Assert.That(sampleData.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetSampleData_GetLocalizationsThrows_BadRequest()
        {
            this.dataAccessLayerMock.Setup(x => x.GetLocalisations()).Throws(new Exception());
            this.sampleDataController = new SampleDataController(this.dataAccessLayerMock.Object);
            var sampleData = this.sampleDataController.GetSampleData();
            Assert.That(sampleData, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetSampleDataSecured_GetLocalisationDoesNotThrow_Ok()
        {
            var sampleData = this.sampleDataController.GetSampleDataSecured();
            Assert.That(sampleData, Is.TypeOf<OkObjectResult>());
        }
    }
}
