using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DirectedGraph.UnitTests
{
    [TestClass()]
    public class DigraphTests
    {
        private StationsList<Station> stations = new StationsList<Station>();

        [TestInitialize]
        public void Initialize()
        {
            
            var input = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            //string[] parsedInputs = input.Split(',');
            string[] parsedInputs = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var parsedInput in parsedInputs)
            {
                var link = parsedInput.Trim();
                var startingStation = stations.AddStation(link.Substring(0, 1));
                var destinationStation = stations.AddStation(link.Substring(1, 1));
                var distance = int.Parse(link.Substring(2, 1));
                startingStation.AddAdjacentStation(destinationStation, distance);
            }

        }

        [TestMethod()]
        public void Test1_RouteDistanceOfGivenRouteABC()
        {
            // 1
            var routeDistanceABC = stations.GetRouteDistance("ABC");
            Assert.AreEqual(9, routeDistanceABC.Distance);
            Assert.AreEqual("ABC", routeDistanceABC.Path);
        }


        [TestMethod()]
        public void Test2_RouteDistanceOfGivenRouteAD()
        {
            var routeDistanceAD = stations.GetRouteDistance("AD");
            Assert.AreEqual(5, routeDistanceAD.Distance);
            Assert.AreEqual("AD", routeDistanceAD.Path);
        }

        [TestMethod()]
        public void Test3_RouteDistanceOfGivenRouteADC()
        {
            var routeDistanceADC = stations.GetRouteDistance("ADC");
            Assert.AreEqual(13, routeDistanceADC.Distance);
            Assert.AreEqual("ADC", routeDistanceADC.Path);
        }

        [TestMethod()]
        public void Test4_RouteDistanceOfGivenRouteAEBCD()
        {
            var routeDistanceAEBCD = stations.GetRouteDistance("AEBCD");
            Assert.AreEqual(22, routeDistanceAEBCD.Distance);
            Assert.AreEqual("AEBCD", routeDistanceAEBCD.Path);
        }

        [TestMethod()]
        public void Test5_RouteDistanceOfGivenRouteAED()
        {
            //Assert.AreEqual("NO SUCH ROUTE", stations.GetRouteDistance("AED"));
            var routeDistanceAED = stations.GetRouteDistance("AED");
            Assert.AreEqual(0, routeDistanceAED.Distance);
            Assert.AreEqual("NO SUCH ROUTE", routeDistanceAED.Path);
        }

        [TestMethod()]
        public void Test6_RouteTripsOfSameStationCWith3MaximumStops()
        {
            Assert.AreEqual("CDC,CEBC", stations.NewGetRouteTripsMaximumStops("CC", 3));
        }

        [TestMethod()]
        public void Test7_RouteTripsOfDifferentStationsACWith4ExactStops()
        {
            
            Assert.AreEqual("ABCDC,ADCDC,ADEBC", stations.NewGetRouteTripsExactStops("AC", 4));
        }

        //[TestMethod()]
        //public void Test7_RouteTripsOfDifferentStationsACWith4ExactStops()
        //{

        //    Assert.AreEqual("ABCDC,ADCDC,ADEBC", stations.NewGetRouteTripsExactStops("AC", 4));
        //    //var routesAC = stations.GetRouteTripsExactStops("AC", 4);
        //    //Assert.AreEqual(3, routesAC.Count());
        //    //Assert.IsTrue(routesAC.Any(x => x.Path == "ABCDC"));
        //    //Assert.IsTrue(routesAC.Any(x => x.Path == "ADCDC"));
        //    //Assert.IsTrue(routesAC.Any(x => x.Path == "ADEBC"));
        //}

        [TestMethod()]
        public void Test8_ShortesRouteDistanceOfDifferentStationsAC()
        {
            Assert.AreEqual(9, stations.NewGetShortestRouteDistance("AC"));
            //var shortestRouteAC = stations.GetShortestRouteBetween("AC");
            //Assert.AreEqual(9, shortestRouteAC.Distance);
            //Assert.AreEqual("ABC", shortestRouteAC.Path);
        }

        [TestMethod()]
        public void Test9_ShortesRouteDistanceOfSameStationB()
        {
            // 9
            Assert.AreEqual(9, stations.NewGetShortestRouteDistance("BB"));
            //var shortestRouteBB = stations.GetShortestRouteBetween("BB");
            //Assert.AreEqual(9, shortestRouteBB.Distance);
            //Assert.AreEqual("BCEB", shortestRouteBB.Path);
            //Assert.AreEqual(9, routeAndDistance.Value);
            //Assert.AreEqual("BCEB", routeAndDistance.Key);
        }

        [TestMethod()]
        public void Test10_ShortesRouteDistanceOfSameStationB()
        {
            //Assert.AreEqual(7, stations.NewGetRoutesMaximumDistance("CC", 30));
            //var trips = stations.GetRouteTripsMaxDistance("CC", 30);                        // 7
            //Assert.AreEqual(7, trips.Count());
            //Assert.IsTrue(trips.Contains("CDC"));
            //Assert.IsTrue(trips.Contains("CEBC"));
            //Assert.IsTrue(trips.Contains("CEBCDC"));
            //Assert.IsTrue(trips.Contains("CDCEBC"));
            //Assert.IsTrue(trips.Contains("CDEBC"));
            //Assert.IsTrue(trips.Contains("CEBCEBC"));
            //Assert.IsTrue(trips.Contains("CEBCEBCEBC"));
        }

    }

}