using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph
{
    public class Station
    {
        public string Name;
        public List<Neighbor> Neighbors = new List<Neighbor>();

        public void AddAdjacentStation(Station neighborStation, int distance)
        {
            if (Neighbors.All(x => x.Station.Name != neighborStation.Name))
            {
                Neighbors.Add(new Neighbor { Distance = distance, Station = neighborStation });
            }
        }

        public int DistanceTo(string destinationStationName)
        {
            var destination = Neighbors.FirstOrDefault(x => x.Station.Name == destinationStationName);
            if(destination == null) 
                throw new Exception(String.Format("Station {0} does not have a destination {1}", Name, destinationStationName));
            return destination.Distance;
        }
    }
}

