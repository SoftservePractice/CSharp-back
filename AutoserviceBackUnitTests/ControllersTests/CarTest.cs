using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoserviceBackCSharp.Controllers;
using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;

namespace AutoserviceBackUnitTests.ControllersTests
{
    public class CarTest
    {
        private CarController carController;
        private Random random;

        [SetUp]
        public void Setup()
        {
            carController = new CarController(PublicContext.context);
            random = new Random();
        }

        [Test]
        public void CarControllerGet_TestNoExceptions()
        {
            Assert.DoesNotThrow(() => carController.GetCars());
        }
        [Test]
        public void CarControllerGet_TestNoNullRes()
        {
            var result = carController.GetCars().Result;

            Assert.IsTrue(result != null);
        }
        [Test]
        public void CarControllerGet_TestIsOkResult()
        {
            var result = carController.GetCars().Result;

            Assert.IsTrue(result is OkObjectResult);
        }
        [Test]
        public void CarControllerGet_TestIsTableHasRows()
        {
            var result = carController.GetCars().Result;
            OkObjectResult okObjRes;
            try
            {
                okObjRes = (OkObjectResult)result!;
                Assert.IsTrue(okObjRes.Value is Array && ((Car[])okObjRes.Value).Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void CarCarControllerPost_TestNoNullRes()
        {
            string mark = "BMW";
            DateTime year = DateTime.Now;
            string vin = random.Next(100000, 999999).ToString();
            string carNumber = random.Next(100000, 999999).ToString();
            int clientID = 2;

            var result = carController.PostCar(mark, year, vin, carNumber, clientID);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CarCarControllerPost_TestIsCreatedResult()
        {
            string mark = "BMW";
            DateTime year = DateTime.Now;
            string vin = random.Next(100000, 999999).ToString();
            string carNumber = random.Next(100000, 999999).ToString();
            int clientID = 2;

            var result = carController.PostCar(mark, year, vin, carNumber, clientID);
            Assert.IsTrue((result as CreatedAtActionResult).StatusCode == (int)HttpStatusCode.Created);
        }

        [Test]
        public void CarControllerUpdate_TestNoNullResult()
        {
            int id = 25;
            string mark = "BMW";
            DateTime year = DateTime.Now;
            string vin = random.Next(100000, 999999).ToString();
            string carNumber = random.Next(100000, 999999).ToString();
            int clientID = 2;

            var result = carController.UpdateCar(id, mark, year, vin, carNumber, clientID);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CarControllerUpdate_TestIsOkResult()
        {
            int id = 25;
            string mark = "BMW";
            DateTime year = DateTime.Now;
            string vin = random.Next(100000, 999999).ToString();
            string carNumber = random.Next(100000, 999999).ToString();
            int clientID = 2;

            var result = carController.UpdateCar(id, mark, year, vin, carNumber, clientID).Result;
            Assert.IsTrue(result is OkObjectResult);
        }

        [Test]
        public void CarControllerDelete_TestNoNullResult()
        {
            int id = 32;

            var result = carController.DeleteCar(id);
            Assert.IsNotNull(result);
        }


        [Test]
        public void CarControllerDelete_TestIsOkResult()
        {
            int id = 33;

            var result = carController.DeleteCar(id);
            Assert.IsTrue(result is OkObjectResult);
        }
    }
}
