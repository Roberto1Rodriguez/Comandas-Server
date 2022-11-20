using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas_Server.Models
{
    public enum Tipo { platillo, refresco };
    public class Producto
    {
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = "";
        public Tipo Tipo { get; set; }

    }
}
