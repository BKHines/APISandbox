using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSandbox.Models
{
    public class Episode
    {
        public string id { get; set; }
        public string showname { get; set; }
        public int season { get; set; }
        public int episode { get; set; }
        public int overall { get; set; }
        public string episodename { get; set; }
        public string airdate { get; set; }
        public bool watched { get; set; }
        public string[] notes { get; set; }
    }
}
