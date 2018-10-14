using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SSO.Interfaces;
using SSO.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SSO.Test
{
    [TestClass]
    public class AppUserRepositoryTest
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return new object[]
            {
                new Dictionary<string, object> {{"Id", 3}},
                new Action<List<SqlParameter>>(x =>
                {
                    Assert.IsTrue(x.Any(st => st.ParameterName == "Id" && st.Value.ToString() == 3.ToString()));
                })
            };
            yield return new object[] { null, new Action<List<SqlParameter>>(x => { Assert.IsTrue(x.Count == 0); }) };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public void ConstructParametersTest(Dictionary<string, object> source, Action<List<SqlParameter>> assert)
        {
            var dataContextMock = new Mock<IDataContext>();
            AppUserRepository ss = new AppUserRepository(dataContextMock.Object);

            assert(ss.ConstructParameters(source));
        }
    }
}
