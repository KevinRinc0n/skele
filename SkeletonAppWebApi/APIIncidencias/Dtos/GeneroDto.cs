using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIncidencias.Dtos
{
    public class GeneroDto
    {
        public int Id { get; set; }
        public string NombreGenero { get; set; }
        public List<GeneroDto> Generos { get; set; } 
    }
}