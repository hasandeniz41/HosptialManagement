using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneAPI.Models
{
    public class Hasta
    {
        public int HastaId { get; set; }
        public string HastaName { get; set; }
        public string Doktor { get; set; }
        public string Bolum { get; set; }
        public string Information { get; set; }
    }
}
