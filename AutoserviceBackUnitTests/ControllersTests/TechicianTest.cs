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
    public class TechicianTest
    {
        public readonly ILogger<TechnicianController>? logger;
        public readonly PracticedbContext? context;
        TechnicianController technicianController;


       [SetUp]
        public void Setup()
        {
            
        }

        public void TechnicianController_Get()
        {
            //technicianController = new TechnicianController(logger, context);
            var result = technicianController.GetTechnicians();
            
            Assert.Pass("123", result);
        }

        public void TechnicianController_Get2()
        {
            //technicianController = new TechnicianController(logger, context);
            int id = 1;
            var result = technicianController.GetTechnician(id);
            Assert.Pass("1233", result);
        }

        public void TechnicianController_Post()
        {
            //technicianController = new TechnicianController(logger, context);
            string name = "sse";
            string phone = "ssw";
            string specialization="ssq";
            DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
            var result = technicianController.PostTechnician(name,phone,specialization,start,end);
            Assert.Pass("1234", result);
        }

        public void TechnicianController_Update()
        {
            //technicianController = new TechnicianController(logger, context);
            int id = 1;
            string name = "sse";
            string phone = "ssw";
            string specialization = "ssq";
            DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
            var result = technicianController.UpdateTechnician(id,name, phone, specialization, start, end);
            Assert.Pass("1231", result);
        }

        public void TechnicianController_Delete()
        {
           //technicianController = new TechnicianController(logger, context);
            int id = 1;
            var result = technicianController.DeleteTechnician(id);
            Assert.Pass("1231", result);
        }
    }
}
