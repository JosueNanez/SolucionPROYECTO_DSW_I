using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appPROYECTO_DSW_I.Models
{
    public class tb_DetalleBoleta
    {
        public int Id { get; set; }
        public int idBoleta { get; set; }
        public String idProducto { get; set; }
        public String nomProducto { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal Monto { get; set; }
    }
}
