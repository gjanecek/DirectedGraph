using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectedGraph
{
    public class StationsList<T> : List<Station>
    {
        public Station AddStation(string stationName)
        {
            var station = this.Find(x => x.Name == stationName);

            if (station != null)
                return station;

            station = new Station { Name = stationName };
            base.Add(station);
            return station;
        }

        public Route GetRouteDistance(string route)
        {
            try
            {
                var stationRoutes = GetStations(route);
                var routeDistance = GetRouteDistance(stationRoutes);
                return new Route(route, routeDistance);
            }
            catch (Exception)
            {
                return new Route("NO SUCH ROUTE", 0);
            }
        }

        //public string GetRouteDistance(string route)
        //{
        //    try
        //    {
        //        var stationRoutes = GetStations(route);
        //        var routeDistance = GetRouteDistance(stationRoutes);
        //        return routeDistance.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        return "NO SUCH ROUTE";
        //    }
        //}

        //public IList<string> GetRouteTrips(string route, int minimumStops, int maximumStops)
        //{
        //    var allRoutes = new List<string>();
        //    if (string.IsNullOrEmpty(route) || route.Length != 2)
        //        return allRoutes;
        //    var routedStations = GetStations(route);
        //    var startingStation = routedStations.First();
        //    var endingStation = routedStations.Last();
        //    //var startingStation = GetStations(route[0].ToString()).FirstOrDefault();
        //    //var endingStation = GetStations(route[1].ToString()).FirstOrDefault();
        //    if (startingStation == null || endingStation == null) return allRoutes;

        //    var currentRoute = string.Empty;
        //    GetRoutes(allRoutes, currentRoute, startingStation, endingStation, minimumStops, maximumStops);
        //    return allRoutes;

        //}


        public IList<Route> GetRouteTripsExactStops(string route, int exactStops)
        {
            var allRoutes = new List<Route>();
            if (string.IsNullOrEmpty(route) || route.Length != 2)
                return allRoutes;
            var routedStations = GetStations(route);
            var startingStation = routedStations.First();
            var endingStation = routedStations.Last();
            if (startingStation == null || endingStation == null) return allRoutes;

            var currentRoute = string.Empty;
            GetRoutesExactNumberOfStops(allRoutes, currentRoute, startingStation, endingStation, exactStops);
            return allRoutes;
        }

        private void GetRoutesExactNumberOfStops(IList<Route> allRoutes, string currentRoute, Station startingStation, Station endingStation, int exactStops)
        {
            if (exactStops < 0) return;
            currentRoute += startingStation.Name;
            if (startingStation == endingStation && exactStops == 0)
            {
                allRoutes.Add(new Route(currentRoute, 0));
                return;
            }

            foreach (var destination in startingStation.Neighbors)
            {
                GetRoutesExactNumberOfStops(allRoutes, currentRoute, destination.Station, endingStation, exactStops - 1);
            }
            return;
        }

        public IList<Route> GetRouteTripsMaximumStops(string route, int maximumStops)
        {
            var allRoutes = new List<Route>();
            if (string.IsNullOrEmpty(route) || route.Length != 2)
                return allRoutes;
            var routedStations = GetStations(route);
            var startingStation = routedStations.First();
            var endingStation = routedStations.Last();
            if (startingStation == null || endingStation == null) return allRoutes;

            var currentRoute = string.Empty;
            GetRoutesMaximumStops(allRoutes, currentRoute, startingStation, endingStation, maximumStops);
            return allRoutes;

        }

        //private void GetRoutes(IList<Route> allRoutes, Route currentRoute, Station startingStation, Station endingStation, int minimumStops, int maximumStops)
        //{
        //    if (maximumStops < 0) return;

        //    currentRoute.Path += startingStation.Name;

        //    if (startingStation == endingStation && !allRoutes.Contains(currentRoute) && minimumStops <= 0)
        //        allRoutes.Add(currentRoute);

        //    foreach (var destination in startingStation.Neighbors)
        //    {
        //        GetRoutes(allRoutes, currentRoute, destination.Station, endingStation, minimumStops - 1, maximumStops - 1);
        //    }
        //    return;
        //}

        //private void GetRoutes(IList<string> allRoutes, string currentRoute, Station startingStation, Station endingStation, int minimumStops, int maximumStops)
        //{
        //    if (maximumStops < 0) return;

        //    currentRoute += startingStation.Name;

        //    if (startingStation == endingStation && !allRoutes.Contains(currentRoute) && minimumStops <= 0)
        //        allRoutes.Add(currentRoute);

        //    foreach (var destination in startingStation.Neighbors)
        //    {
        //        GetRoutes(allRoutes, currentRoute, destination.Station, endingStation, minimumStops - 1, maximumStops - 1);
        //    }
        //    return;
        //}

        private void GetRoutesMaximumStops(IList<Route> allRoutes, string currentRoute, Station startingStation, Station endingStation, int maximumStops)
        {
            if (maximumStops < 0) return;

            currentRoute += startingStation.Name;

            if (startingStation == endingStation && currentRoute.Length > 1)
                allRoutes.Add(new Route(currentRoute, 0));

            foreach (var destination in startingStation.Neighbors)
            {
                GetRoutesMaximumStops(allRoutes, currentRoute, destination.Station, endingStation, maximumStops - 1);
            }
            return;
        }

        private void FindAllNonCyclicRoutesBetween(IList<Route> stationRoutes, string currentRoute, Station startingStation, Station endingStation)
        {
            if (currentRoute.Contains(startingStation.Name))
                return;

            currentRoute += startingStation.Name;

            if (startingStation == endingStation && currentRoute.Length > 1)
            {
                stationRoutes.Add(new Route(currentRoute, 0));
                return;
            }

            foreach (var neighbor in startingStation.Neighbors.OrderByDescending(x => x.Distance))
            {
                FindAllNonCyclicRoutesBetween(stationRoutes, currentRoute, neighbor.Station, endingStation);
            }
            return;

        }

        //public KeyValuePair<string, int> GetShortestDistance(string route)
        //{
        //    var routeStations = GetStations(route);
        //    if (routeStations.Count != 2)
        //        return new KeyValuePair<string, int>("Invalid Route", 0);

        //    KeyValuePair<string, int> routeAndDistance;

        //    if (route.Distinct().Count() > 1)
        //    {
        //        routeAndDistance = FindShortestDistance(routeStations.First(), routeStations.Last());
        //    }
        //    else
        //    {
        //        var abc = new List<KeyValuePair<string, int>>();
        //        var targetStation = routeStations.Skip(1).First();
        //        foreach (var neighbor in routeStations.First().Neighbors.OrderByDescending(x => x.Distance))
        //        {
        //            var insideRouteStations = new List<Station> { neighbor.Station, targetStation };
        //            var getRouteAndDistance = FindShortestDistance(insideRouteStations);
        //            abc.Add(new KeyValuePair<string, int>(routeStations.First().Name + getRouteAndDistance.Key, neighbor.Distance + getRouteAndDistance.Value));
        //        }
        //        var shortestRoute = abc.First();
        //        var shortestDistance = abc.First().Value;
        //        foreach (var kvp in abc)
        //        {
        //            if (kvp.Value < shortestDistance)
        //            {
        //                shortestRoute = kvp;
        //                shortestDistance = kvp.Value;
        //            }
        //        }
        //        routeAndDistance = shortestRoute;
        //    }
        //    return routeAndDistance;
        //}

        //private KeyValuePair<string, int> FindShortestDistance(Station startingStation, Station EndingStation)
        //{
        //    var currentRoute = string.Empty;
        //    var allRoutes = new List<String>();
        //    FindAllNonCyclicRoutesBetween(allRoutes, currentRoute, startingStation, EndingStation);
        //    if (!allRoutes.Any()) return new KeyValuePair<string, int>(null, 0);
        //    var shortestRoute = allRoutes.First();
        //    var shortestDistance = GetRouteDistance(GetStations(allRoutes.First()));
        //    foreach (var thisRoute in allRoutes.OrderBy(x => x.Length))
        //    {
        //        var distance = GetRouteDistance(GetStations(thisRoute));
        //        if (distance < shortestDistance)
        //        {
        //            shortestDistance = distance;
        //            shortestRoute = thisRoute;
        //        }
        //    }
        //    return new KeyValuePair<string, int>(shortestRoute, shortestDistance);
        //}
        //private KeyValuePair<string, int> FindShortestDistance(IList<Station> routeStations)
        //{
        //    var currentRoute = string.Empty;
        //    var allRoutes = new List<String>();
        //    FindAllNonCyclicRoutesBetween(allRoutes, currentRoute, routeStations.First(), routeStations.Last());
        //    if (!allRoutes.Any()) return new KeyValuePair<string, int>(null, 0);
        //    var shortestRoute = allRoutes.First();
        //    var shortestDistance = GetRouteDistance(GetStations(allRoutes.First()));
        //    foreach (var thisRoute in allRoutes.OrderBy(x => x.Length))
        //    {
        //        var distance = GetRouteDistance(GetStations(thisRoute));
        //        if (distance < shortestDistance)
        //        {
        //            shortestDistance = distance;
        //            shortestRoute = thisRoute;
        //        }
        //    }
        //    return new KeyValuePair<string, int>(shortestRoute, shortestDistance);
        //}


        private int GetRouteDistance(IList<Station> stations)
        {
            var distance = 0;
            if (stations.Count < 2)
                return distance;
            var fromStation = stations.First();

            foreach (var station in stations.Skip(1))
            {
                distance += fromStation.DistanceTo(station.Name);
                fromStation = station;
            }
            return distance;
        }

        public IList<Station> GetStations(string route)
        {
            return route.Select(c => this.First(x => x.Name == c.ToString())).ToList();
        }

        public IList<Route> GetRouteTripsMaxDistance(string route, int maximumDistance)
        {
            var allRoutes = new List<Route>();
            if (string.IsNullOrEmpty(route) || route.Length != 2)
                return allRoutes;
            var routedStations = GetStations(route);
            var startingStation = routedStations.First();
            var endingStation = routedStations.Last();
            if (startingStation == null || endingStation == null)
                return allRoutes;

            foreach (var neighbor in startingStation.Neighbors)
            {
                var currentRoute = new Route(startingStation.Name, 0);
                GetRoutesMaximumDistance(allRoutes, currentRoute, neighbor.Station, endingStation, maximumDistance - neighbor.Distance);
            }
            return allRoutes;




        }

        private void GetRoutesMaximumDistance(IList<Route> allRoutes, Route currentRoute, Station startingStation, Station endingStation, int maximumDistance)
        {
            if (maximumDistance < 0)
                return;

            currentRoute.Path += startingStation.Name;

            if (startingStation == endingStation && maximumDistance > 0)
                allRoutes.Add(currentRoute);

            foreach (var neighbor in startingStation.Neighbors)
            {
                GetRoutesMaximumDistance(allRoutes, currentRoute, neighbor.Station, endingStation, maximumDistance - neighbor.Distance);
            }
        }


        public Route GetShortestRouteBetween(string route)
        {
            var routeStations = GetStations(route);
            if (routeStations.Count != 2)
                throw new Exception(String.Format("Invalid Route {0}.  Must have exactly 2 stations (could be identical).", route));

            var allRoutes = new List<Route>();

            if (route.Distinct().Count() == 2)
            {
                var currentRoute = string.Empty;
                NewFindAllNonCyclicRoutesBetween(allRoutes, currentRoute, routeStations.First(), routeStations.Last());
            }
            else
            {
                foreach (var neighbor in routeStations.First().Neighbors)
                {
                    var currentRoute = routeStations.First().Name;
                    NewFindAllNonCyclicRoutesBetween(allRoutes, currentRoute, neighbor.Station, routeStations.Last());
                }
            }
            return GetShortestRoute(allRoutes);
        }

        private void NewFindAllNonCyclicRoutesBetween(List<Route> allRoutes, string currentRoute, Station startingStation, Station endingStation)
        {
            if (startingStation == endingStation && currentRoute.Contains(startingStation.Name))
                return;
            currentRoute += startingStation.Name;
            if (startingStation == endingStation)
            {
                allRoutes.Add(new Route(currentRoute, 0));
                return;
            }
            foreach (var neighbor in startingStation.Neighbors)
            {
                NewFindAllNonCyclicRoutesBetween(allRoutes, currentRoute, neighbor.Station, endingStation);
            }
        }

        private Route FindShortestNonCyclicRouteBetween(Station startingStation, Station EndingStation)
        {
            var currentRoute = string.Empty;
            var allRoutes = new List<Route>();
            FindAllNonCyclicRoutesBetween(allRoutes, currentRoute, startingStation, EndingStation);
            if (!allRoutes.Any()) return new Route();

            return GetShortestRoute(allRoutes);
        }

        private Route FindShortestCyclicRoute(Station targetStation)
        {
            var shortestRoute = new Route();
            var startingStation = targetStation;
            var availableRoutes = new List<Route>();
            var currentRoute = string.Empty;
            FindAllNonCyclicRoutesBetween(availableRoutes, currentRoute, startingStation, targetStation);

            foreach (var neighbor in startingStation.Neighbors.OrderByDescending(x => x.Distance))
            {
                availableRoutes.Add(FindShortestNonCyclicRouteBetween(neighbor.Station, targetStation));
            }
            return GetShortestRoute(availableRoutes);
        }

        private Route GetShortestRoute(IList<Route> availableRoutes)
        {
            if (!availableRoutes.Any())
                return new Route();

            // Seed : Assume first (any) route is shortest route
            var shortestRoute = availableRoutes.First().Path;
            var shortestDistance = GetRouteDistance(GetStations(shortestRoute));

            foreach (var thisRoute in availableRoutes)
            {
                var distance = GetRouteDistance(GetStations(thisRoute.Path));
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestRoute = thisRoute.Path;
                }
            }
            return new Route(shortestRoute, shortestDistance);

        }

    }
}