using BusCache.Comum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BusCache.Comum.Collections
{
    public class ComandosEntradaCollection : KeyedCollection<string, ComandosEntradaModel>
    {
        protected override string GetKeyForItem(ComandosEntradaModel item)
        {
            return item.Comando;
        }
    }
}
