using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Collections.Generic;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);
            if(lines == null || lines.Length == 0)
            {
                logger.LogError("Your file is empty.");
                return;
            }
            if(lines.Length == 1)
            {
                logger.LogWarning("Warning: File only has one record.");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS
            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tbOne = new TacoBell();
            ITrackable tbTwo = new TacoBell();
            Point pntOne = new Point();
            Point pntTwo = new Point();
            double distance = 0;
            double tempDistance = 0;

            var locA = new GeoCoordinate();
            var locB = new GeoCoordinate();

            for(int i = 0; i < locations.Length; i++)
            {
                locA.Latitude = locations[i].Location.Latitude;
                locA.Longitude = locations[i].Location.Longitude;
                for(int j = 0; j < locations.Length; j++)
                {
                    locB.Latitude = locations[j].Location.Latitude;
                    locB.Longitude = locations[j].Location.Longitude;

                    tempDistance = locA.GetDistanceTo(locB);
                    distance = Math.Max(tempDistance, distance);

                    if(tempDistance > distance)
                    {
                        distance = tempDistance;
                        tbOne.Name = locations[i].Name;
                        pntOne.Latitude = locations[i].Location.Latitude;
                        pntOne.Longitude = locations[i].Location.Longitude;
                        tbOne.Location = pntOne;

                        tbTwo.Name = locations[j].Name;
                        pntTwo.Latitude = locations[j].Location.Latitude;
                        pntTwo.Longitude = locations[j].Location.Longitude;
                        tbTwo.Location = pntTwo;

                    }
                }
            }
            logger.LogInfo($"The two furthest Taco Bells in AL are in {tbOne.Name} and {tbTwo.Name} and are {distance * 0.000621371} miles apart.");

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.


            
        }
    }
}
