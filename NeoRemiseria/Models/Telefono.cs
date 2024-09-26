using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models{

public partial class Telefono{
    public uint Id { get; set;}
    public String? Numero {get;set;}
    public char Estado {get;set;}='A';
    
}



}