using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ServicioAPI.Entities
{
    public class tb_DetalleBoleta
    {
        [Key]
        public int Id { get; set; }
        public int idBoleta { get; set; }
        public String idProducto { get; set; }
        public String nomProducto { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal Monto { get; set; }


    }
}
