using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models{
    public partial class Telefono{
        public uint Id {get; set;}
        public string Numero {get; set;} = null!;
        public char Estado {get; set;} = 'A';
        public uint IdPersona {get; set;}

        public virtual Persona? IdPersonaNavigation {get; set;}
    }
}