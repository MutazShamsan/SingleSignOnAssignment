using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSO.Interfaces;
using SSO.Service.Logic;
using System;
using System.Collections.Generic;

namespace SSO.Test
{
    [TestClass]
    public class LoginManagementTest
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


        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { "Test", new Action<string>(x => { Assert.IsTrue(x.Contains("Test")); }) };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public void CreateAccessTokenTest(string data, Action<string> assert)
        {
            LoginManagement login = new LoginManagement(appuserRepositoryMock.Object, loggerMock.Object,
                cryptoMock.Object, cacheManagementMock.Object);

            var value = login.CreateAccessToken(data);

            assert(value);

        }
    }
}
