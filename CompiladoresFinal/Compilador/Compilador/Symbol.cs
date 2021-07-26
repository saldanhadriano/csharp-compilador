using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador    
{
    class Symbol
    {
        private String local;
        private String tipo;
        private String indice;

        public string Local { get => local; set => local = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Indice { get => indice; set => indice = value; }
    }
}
