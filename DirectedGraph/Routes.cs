using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph
{
    public class Routes<T> : List<Route>
    {
        //public void AddRoute(Station startingStation, Station endingStation, int distance)
        //{
        //    base.Add(new Route(startingStation, endingStation, distance));
        //}

        public Routes(List<List<MyRoute>> validRoutes)
        {
            //var routes = new List<Route>();
            foreach (var validRoute in validRoutes)
            {
                var newRoute = new Route();
                int distance = 0;
                foreach (var myRoute in validRoute)
                {
                    newRoute.Path += myRoute.StartingStation.Name;
                    distance += myRoute.Distance;
                }
                newRoute.Path += validRoute.Last().EndingStation.Name;
                this.Add(new Route(newRoute.Path, distance));
            }
            //return routes;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i].Path);
                if (i != this.Count - 1)
                    sb.Append(",");
            }
            return sb.ToString();
            //var sb = new StringBuilder();
            //var distance = 0;
            //foreach(var route in this)
            //{
            //    sb.Append(route.StartingStation.Name);
            //    distance += route.Distance;
            //}
            //sb.Append(this.Last().EndingStation.Name);
            //sb.Append(" - ");
            //sb.Append(distance);
            //return sb.ToString();
        }
    }
}
