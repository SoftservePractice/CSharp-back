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
    public class CarTest
    {
        public readonly ILogger<CarController>? logger;
        public readonly PracticedbContext? context;
        CarController carController;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CarController_Get()
        {
            carController = new CarController(logger, context);
            var result = carController.GetCars();

            Assert.Pass("123", result);
        }

        public void CarController_Get2()
        {
            carController = new CarController(logger, context);
            int id = 1;
            var result = carController.GetCar(id);
            Assert.Pass("1233", result);
        }

        public void CarController_Post()
        {
            carController = new CarController(logger, context);
            string mark = "sse";
            DateTime year = new DateTime(2008, 3, 1, 7, 0, 0);
            string vin = "dfs";
            string carNumber = "ssq";
            int clientID = 1;

            var result = carController.PostCar(mark, year, vin, carNumber, clientID);
            Assert.Pass("1234", result);
        }

        public void CarController_Update()
        {
            carController = new CarController(logger, context);
            int id = 1;
            string mark = "sse";
            DateTime year = new DateTime(2008, 3, 1, 7, 0, 0);
            string vin = "dfs";
            string carNumber = "ssq";
            int clientID = 1;
            var result = carController.UpdateCar(id, mark, year, vin, carNumber, clientID);
            Assert.Pass("1231", result);
        }

        public void CarController_Delete()
        {
            carController = new CarController(logger, context);
            int id = 1;
            var result = carController.DeleteCar(id);
            Assert.Pass("1231", result);
        }
    }
}
