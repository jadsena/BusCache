using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Comandos.Models
{
    public class ComandoModel
    {
        public string Comando { get; set; }
        public string Parametros { get; set; }
        /// <summary>
        /// Converte string em ComandoModel
        /// </summary>
        /// <param name="mensagem">string</param>
        /// <returns>ComandoModel</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static ComandoModel Parse(string mensagem)
        {
            if (string.IsNullOrWhiteSpace(mensagem)) throw new ArgumentNullException(nameof(mensagem));
            string[] partes = mensagem.Split(' ');
            return new ComandoModel { Comando = partes[0], Parametros = partes.Length > 1 ? string.Join(" ", partes, 1, partes.Length - 1) : "" };
        }
    }
}
