using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas_Server.Models
{
    public class Comanda
    {
        public int Id { get; set; }

        public Dictionary<string, Producto> Pedidos { get; set; }

        public decimal Total { get; set; }

        public string Fecha { get; set; }

        public string Notas { get; set; }

        public string Mesa { get; set; }
    }
}
