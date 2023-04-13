using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceBackCSharp.Controllers;

namespace AutoserviceBackUnitTests.ControllersTests
{
    public class TechicianTest
    {
        TechnicianController technicianController;

        [SetUp]
        public void Setup()
        {
            TechnicianController technicianController;
        }

        [Test]
        public void TechnicianController_Get()
        {
            var result = technicianController.GetTechnicians();
            Assert.IsInstanceOf<TechnicianController>(result);
        }

        public void TechnicianController_Get2()
        {
            int id = 1;
            var result = technicianController.GetTechnician(id);
            Assert.IsInstanceOf<TechnicianController>(result);
        }

        public void TechnicianController_Post()
        {
            string name = "sse";
            string phone = "ssw";
            string specialization="ssq";
            DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
            var result = technicianController.PostTechnician(name,phone,specialization,start,end);
            Assert.IsInstanceOf<TechnicianController>(result);
        }

        public void TechnicianController_Update()
        {
            int id = 1;
            string name = "sse";
            string phone = "ssw";
            string specialization = "ssq";
            DateTime start = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime end = new DateTime(2009, 3, 1, 7, 0, 0);
            var result = technicianController.UpdateTechnician(id,name, phone, specialization, start, end);
            Assert.IsInstanceOf<TechnicianController>(result);
        }

        public void TechnicianController_Delete()
        {
            int id = 1;
            var result = technicianController.DeleteTechnician(id);
            Assert.IsInstanceOf<TechnicianController>(result);
        }
    }
}
