using System;
using System.Collections.Generic;
using System.Text;

namespace TVmaze.Domain.Models
{

    public class CastDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
    }

    public class ShowDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<CastDTO> cast { get; set; }
    }
}
