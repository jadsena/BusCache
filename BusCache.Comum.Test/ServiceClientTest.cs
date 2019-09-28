using BusCache.Comum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
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
            //Arrange
            string expected = "Mensagem teste";
            string actual = "";
            var mockStream = new Mock<Stream>();
            mockStream.Setup(str => str.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Callback<byte[], int, int>((buff, offset,size) =>
            {
                actual = Encoding.UTF8.GetString(buff);
                Console.WriteLine(actual);
            });
            var client = new ServiceClient { Name = "Teste Client", ServiceName = "Service1" };
            //Act
            client.SendData(mockStream.Object, expected);
            //Assert
            Assert.IsTrue(actual.Contains(expected));
        }
    }
}
