using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph
{
    public class Route
    {
        public string Path;
        public int Distance;

        public Route() {}

        public Route(string path, int distance)
        {
            Path = path;
            Distance = distance;
        }
    }
}
