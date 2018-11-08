using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Xunit;

namespace Wetr.Server.Tests
{ 
    public class ConnectionTest
    {
        private IConnectionFactory connFac;
        public ConnectionTest()
        {
            connFac = ConnectionFactory.CreateFromConfiguration("WeatherTracer");
        }

        [Fact]
        public void TestConnection()
        {
            Assert.True(connFac != null);
        }
    }
}
