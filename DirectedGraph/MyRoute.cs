using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph
{
    public class MyRoute
    {
        public Station StartingStation { get; set; }
        public Station EndingStation { get; set; }
        public int Distance { get; set; }

        public MyRoute(Station startingStation, Station endingStation, int distance)
        {
            StartingStation = startingStation;
            EndingStation = endingStation;
            Distance = distance;
        }
    }
}
