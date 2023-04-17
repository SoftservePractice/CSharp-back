﻿using AutoserviceBackCSharp.Controllers;
using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackUnitTests.ControllersTests
{
    public class WorkTest
    {
        private CarController carController;

        [SetUp]
        public void Setup()
        {
            carController = new CarController(null, PublicContext.context);
        }

        
        [Test]
        public void WorkControllerGet_TestNoExceptions()
        {
            Assert.DoesNotThrow(() => carController.GetCars());
        }
        [Test]
        public void WorkControllerGet_TestNoNullRes()
        {
            var result = carController.GetCars().Result;

            Assert.IsTrue(result != null);
        }
        [Test]
        public void WorkControllerGet_TestIsOkResult()
        {
            var result = carController.GetCars().Result;

            Assert.IsTrue(result is OkObjectResult);
        }
        [Test]
        public void WorkControllerGet_TestIsTableHasRows()
        {
            var result = carController.GetCars().Result;
            if(result == null)
            {
                Assert.Fail();
            }
            var okObjRes = (OkObjectResult)result;
            var tedfs = ((Car[])okObjRes.Value);
            Assert.IsTrue(okObjRes.Value is Array && ((Car[])okObjRes.Value).Length > 0);
        }
    }
}
