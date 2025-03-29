
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practicaprogra.Models
{
    public class CambioModel
    {
        public string PaisOrigen { get; set; }
        public string PaisDestino { get; set; }
        public string MonedaRecibir { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Resultado { get; set; }
    }
}