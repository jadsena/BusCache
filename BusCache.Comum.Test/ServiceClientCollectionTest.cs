using BusCache.Comum.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;

namespace BusCache.Comum.Test
{
    [TestClass]
    public class ServiceClientCollectionTest
    {
        [TestMethod]
        public void GetClientByNameNullTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" }
            };
            //Act
            var srv = serviceClients.GetClientByName("Service2");

            //Assert
            Assert.IsNull(srv);
        }
        [TestMethod]
        public void GetClientByNameNotNullTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(),It.IsAny<EventId>(),It.IsAny<object>(),It.IsAny<Exception>(),It.IsAny<Func<object, Exception,string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) => 
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });

            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" }
            };
            //Act
            var srv = serviceClients.GetClientByName("Service1");

            //Assert
            Assert.IsNotNull(srv);
        }
        [TestMethod]
        public void GetClientsByNameNullTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" }
            };
            //Act
            var srv = serviceClients.GetClientByName("Service2");

            //Assert
            Assert.IsNull(srv);
        }
        [TestMethod]
        public void GetClientsByNameNotNullTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" }
            };
            //Act
            var srv = serviceClients.GetClientsByName("Service1");

            //Assert
            Assert.IsNotNull(srv);
        }
        [TestMethod]
        public void GetClientsByNameNotNullFiltroNuloTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" }
            };
            //Act
            var srv = serviceClients.GetClientsByName("");

            //Assert
            Assert.IsNotNull(srv);
        }
        [TestMethod]
        public void UpdateNameTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" }
            };
            //Act
            serviceClients.UpdateName(serviceClients.GetClientByName("Service1"), "Service2");
            var srv = serviceClients.GetClientByName("Service2");

            //Assert
            Assert.IsTrue(srv.Name.Equals("Service2"));
        }
        [TestMethod]
        public void UpdateNameMesmoNomeTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" },
                new Models.ServiceClient { Client = null, Name = "Service2", ServiceName = "Teste2" }
            };
            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(()=> serviceClients.UpdateName(serviceClients.GetClientByName("Service1"), "Service2"));
        }
        [TestMethod]
        public void UpdateNameClientDesconectadoTest()
        {
            //Arrange
            var mock = new Mock<ILogger<ServiceClientCollection>>();
            mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                {
                    string msg = func.Invoke(obj, exception);
                    Console.WriteLine(msg);
                });
            ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
            {
                new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" },
                new Models.ServiceClient { Client = null, Name = "Service2", ServiceName = "Teste2" }
            };
            //Act
            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => serviceClients.UpdateName(new Models.ServiceClient { Client = null, Name = "Service3", ServiceName = "Teste3" }, "Service2"));
        }

        [TestMethod]
        public void AddMesmoNomeTest()
        {
            //Arrange
            //Act
            //Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var mock = new Mock<ILogger<ServiceClientCollection>>();
                mock.Setup(m => m.Log<object>(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                    .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, obj, exception, func) =>
                    {
                        string msg = func.Invoke(obj, exception);
                        Console.WriteLine(msg);
                    });
                ServiceClientCollection serviceClients = new ServiceClientCollection(mock.Object)
                {
                    new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste1" },
                    new Models.ServiceClient { Client = null, Name = "Service1", ServiceName = "Teste2" }
                };
            });
        }

    }
}
