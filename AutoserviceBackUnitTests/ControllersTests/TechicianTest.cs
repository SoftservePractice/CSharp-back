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

namespace AutoserviceBackUnitTests.ControllersTests
{
    public class TechicianTest
    {
        private TechnicianController techController;


        [SetUp]
        public void Setup()
        {
            techController = new TechnicianController(PublicContext.context);
        }
        [Test]
        public void TechnicianController_Get()
        {
            //technicianController = new TechnicianController(logger, context);
            Assert.DoesNotThrow(() => techController.GetTechnicians());
        }
        [Test]
        public void TechnicianController_Get2()
        {
            var result = techController.GetTechnicians();

            Assert.IsTrue(result != null);
        }

        //[Test]
        //public void TechnicianController_Get3()
        //{
        //    var result = techController.GetTechnicians();

        //    Assert.IsTrue(result is OkObjectResult);
        //}
        //[Test]
        //public void TechnicianController_Post()
        //{
        //    //technicianController = new TechnicianController(logger, context);
        //    string name = "sse";
        //    string phone = "380959072724";
        //    string specialization = "ssq";
        //    DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
        //    DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
        //    var result = techController.PostTechnician(name, phone, specialization, start, end);
        //    Assert.IsTrue((result as CreatedAtActionResult).StatusCode == (int)HttpStatusCode.Created);
        //}

        //[Test]
        //public void TechnicianController_Post2()
        //{
        //    //technicianController = new TechnicianController(logger, context);
        //    string name = "sse";
        //    string phone = "380959072724";
        //    string specialization = "ssq";
        //    DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
        //    DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
        //    var result = techController.PostTechnician(name, phone, specialization, start, end);
        //    Assert.IsNotNull(result);
        //}

        //public void TechnicianController_Update()
        //{
        //    //technicianController = new TechnicianController(logger, context);
        //    int id = 1;
        //    string name = "sse";
        //    string phone = "ssw";
        //    string specialization = "ssq";
        //    DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
        //    DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
        //    var result = techController.UpdateTechnician(id,name, phone, specialization, start, end);
        //    Assert.IsTrue(result is OkObjectResult);
        //}

        //public void TechnicianController_Update2()
        //{
        //    //technicianController = new TechnicianController(logger, context);
        //    int id = 1;
        //    string name = "sse";
        //    string phone = "ssw";
        //    string specialization = "ssq";
        //    DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
        //    DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
        //    var result = techController.UpdateTechnician(id, name, phone, specialization, start, end);
        //    Assert.IsNotNull(result);
        //}

        //public void TechnicianController_Delete()
        //{
        //   //technicianController = new TechnicianController(logger, context);
        //    int id = 1;
        //    var result = techController.DeleteTechnician(id);
        //    Assert.IsNotNull(result);
        //}
    }
}
