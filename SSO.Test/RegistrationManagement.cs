using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSO.Interfaces;
using SSO.Service.Logic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSO.DataModel;

namespace SSO.Test
{
    [TestClass]
    public class RegistrationManagementTest
    {
        private Mock<IAppUserRepository> appuserRepositoryMock;
        private Mock<ILogger> loggerMock;
        Mock<ICrypto> cryptoMock;

        private Mock<ICacheManagement> cacheManagementMock;
        [TestInitialize]
        public void TestInitialize()
        {
            appuserRepositoryMock = new Mock<IAppUserRepository>();
            loggerMock = new Mock<ILogger>();
            cryptoMock = new Mock<ICrypto>();
            cacheManagementMock = new Mock<ICacheManagement>();
        }

        [TestMethod]
        public async Task GenerateHashedPasswordTest()
        {
            cryptoMock.Setup(st => st.EncryptPassword("", new byte[16])).Returns(Task.FromResult(new Dictionary<string, byte[]>()));

            RegistrationManagement login = new RegistrationManagement(appuserRepositoryMock.Object, loggerMock.Object, cryptoMock.Object);

            var value = await login.GenerateHashedPassword(new RegistrationRequestModel());

           Assert.AreEqual(value, false);

        }
    }
}
