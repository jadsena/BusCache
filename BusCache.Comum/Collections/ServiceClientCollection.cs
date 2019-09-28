using BusCache.Comum.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusCache.Comum.Collections
{
    public class ServiceClientCollection : KeyedCollection<string, ServiceClient>
    {
        private ILogger<ServiceClientCollection> Logger { get; }

        public ServiceClientCollection(ILogger<ServiceClientCollection> logger)
        {
            Logger = logger;
        }
        public IList<ServiceClient> GetClientsByName(string Filtro)
        {
            if (string.IsNullOrWhiteSpace(Filtro)) return GetAllClients();
            List<ServiceClient> list = new List<ServiceClient>();
            return GetAllClients().Where(item => item.Name.ToLower().Contains(Filtro.ToLower())).ToList();
        }
        public IList<ServiceClient> GetAllClients() => Items;
        protected override string GetKeyForItem(ServiceClient item)
        {
            var it = GetClientByName(item.Name);
            if (GetClientByName(item.Name) is null || it.ServiceName.Equals(item.ServiceName))
                return item.ServiceName;
            throw new ArgumentException($"Nome [{item.Name}] já faz parte dos serviços cadastrados.", nameof(item));
        }
        /// <summary>
        /// Troca o nome de exibição do serviço
        /// </summary>
        /// <param name="client">Cliente que terá o nome trocado</param>
        /// <param name="newName">Novo nome atribuído</param>
        /// <exception cref="ArgumentException">Nome não faz parte dos serviços cadastrados.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Nome não faz parte dos serviços cadastrados.</exception>
        public void UpdateName(ServiceClient client, string newName)
        {
            if (Items.Contains(client))
            {
                if (GetClientByName(newName) is null)
                {
                    Logger.LogInformation("Troca de nome...");
                    Remove(client);
                    Add(new ServiceClient { Client = client.Client, ServiceName = client.ServiceName, Name = newName, Channels = client.Channels });
                }
                else
                    throw new ArgumentException($"Nome [{newName}] já faz parte dos serviços cadastrados.", nameof(newName));
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(newName), "Nome não faz parte dos serviços cadastrados.");
            }
        }
        /// <summary>
        /// Encontra um item pelo seu displayname
        /// </summary>
        /// <param name="name">displayname a ser encontrado</param>
        /// <returns><see cref="ServiceClient"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ServiceClient GetClientByName(string name)
        {
            Logger.LogInformation($"Getname {name}");
            return GetAllClients().FirstOrDefault(item => item.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
