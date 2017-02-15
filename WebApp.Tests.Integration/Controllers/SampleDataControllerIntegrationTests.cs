using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.Models.DataDb;

namespace WebApp.Tests.Integration.Controllers
{
    [TestFixture]
    public class SampleDataControllerIntegrationTests
    {
        private SampleDataController sampleDataController;

        [SetUp]
        public void Setup()
        {
            this.sampleDataController = new SampleDataController(DbContextFactory.DataAccessLayerInstance);
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
            var sampleData = (IList<Localizations>)((OkObjectResult)this.sampleDataController.GetSampleData()).Value;
            Assert.That(sampleData.Count, Is.EqualTo(6));
        }

        [Test]
        public void GetSampleDataSecured_GetLocalisationDoesNotThrow_Ok()
        {
            var sampleData = this.sampleDataController.GetSampleDataSecured();
            Assert.That(sampleData, Is.TypeOf<OkObjectResult>());
        }
    }
}
