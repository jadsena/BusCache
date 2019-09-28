using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BusCache.Comum.Test
{
    [TestClass]
    public class ServiceClientTest
    {
        [TestMethod]
        public void SendDataTest()
        {
            var mock = new Mock<NetworkStream>();
            mock.Setup(p => p.Write(It.IsAny<byte[]>(),It.IsAny<int>(), It.IsAny<int>())).Callback<byte[]>((buff) => { Console.WriteLine(buff); });
        }
    }
}
