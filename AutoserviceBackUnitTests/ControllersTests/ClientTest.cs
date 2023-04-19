using AutoserviceBackCSharp.Controllers;
using AutoserviceBackCSharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoserviceBackUnitTests.ControllersTests
{
    public class ClientTest
    {
        private ClientController clientController;

       [SetUp]
        public void Setup()
        {
            clientController = new ClientController(PublicContext.context);
        }

        [Test]
        public void ClientControllerGet_TestNoExceptions()
        {
            Assert.DoesNotThrow(() => clientController.GetClients(null, null, null, null));
        }
        [Test]
        public void ClientControllerGet_TestNoNullRes()
        {
            var result = clientController.GetClients(null, null, null, null).Result;

            Assert.IsTrue(result != null);
        }
        [Test]
        public void ClientControllerGet_TestIsOkResult()
        {
            var result = clientController.GetClients(null, null, null, null).Result;

            Assert.IsTrue(result is OkObjectResult);
        }
        [Test]
        public void ClientControllerGet_TestIsTableHasRows()
        {
            var result = clientController.GetClients(null, null, null, null).Result;
            OkObjectResult okObjRes;
            try
            {
                okObjRes = (OkObjectResult)result!;
                Assert.IsTrue(okObjRes.Value is Array && ((Client[])okObjRes.Value).Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
           
        }
    }
}
