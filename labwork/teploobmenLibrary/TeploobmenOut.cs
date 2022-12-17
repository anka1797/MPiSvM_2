using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teploobmenLibrary
{
    public class TeploobmenOut
    {
        public double square { get; set; }
        public double relationship { get; set; }
        public double layer_height { get; set; }
        public List<double> coordinates { get; set; }
        public List<double> Y { get; set; }
        public List<double> exp1 { get; set; }
        public List<double> exp2 { get; set; }
        public List<double> V { get; set; }
        public List<double> O { get; set; }
        public List<double> t { get; set; }
        public List<double> T { get; set; }
        public List<double> t_T { get; set; }

    }
}
