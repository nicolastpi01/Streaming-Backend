using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class MediaEntity : Entity
    {
        //1 
        public int idComision { get; set; }
        public string codigoComision { get; set; }
        public string sistema { get; set; }
        public string producto { get; set; }
        public string concepto { get; set; }
        public string segmento { get; set; }
        public string canal { get; set; }
        public double importeHasta { get; set; }
        public double importeDesde { get; set; }
        public double porcentajeComision { get; set; }
        public bool esMontoFijo { get; set; }
        public string CUIT { get; set; }
        public string TT { get; set; }

    }
}


