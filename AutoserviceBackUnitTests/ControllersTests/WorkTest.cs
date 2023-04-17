using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceBackCSharp.Controllers;
using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoserviceBackUnitTests.ControllersTests
{
    public class WorkTest
    {
        private CarController carController;

        [SetUp]
        public void Setup()
        {
            carController = new CarController(null, new PracticedbContext(null, new AutoserviceBackCSharp.Singletone.DbConnection()));
        }

        [Test]
        public void WorkControllerGet_TestNoErrors()
        {
            carController = new CarController(logger, context);
            var result = carController.GetCars();

            Assert.Pass("123", result);
        }

    }
}
