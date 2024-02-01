using Nearest_Vehicle_Positions;
using System.Diagnostics;

namespace VehiclePosition_Application
{
    internal class FileLookup
    {
        internal static void GetClosestDistance()
        {
            // Load data from binary file
            List<VehiclePosition> positions = FileReader.ReadDataFile();

            // Create a spatial index using a Quadtree
            SpatialIndex spatialIndex = new SpatialIndex(positions);

            // Start Timer
            Stopwatch watch = Stopwatch.StartNew();

            // Find the closest positions for each coordinate
            string result = "";
            foreach (var coordinate in GetStaticPositions())
            {
                int nearestPositionId = spatialIndex.FindNearestPositionId(coordinate);
                result += $"Position ID for ({coordinate.Latitude}, {coordinate.Longitude}): {nearestPositionId}\n";
            }

            watch.Stop();
            Console.WriteLine(result);
            Console.WriteLine(" ");
            Console.WriteLine($"Execution time: {watch.ElapsedMilliseconds} ms");
        }

        static Coordinate[] GetStaticPositions()
        {
            // Coordinates to find closest positions
            var coordinates = new[]
            {
                new Coordinate(34.544909, -102.100843),
                new Coordinate(32.345544, -99.123124),
                new Coordinate(33.234235f, -100.214124f),
                new Coordinate(35.195739f, -95.348899f),
                new Coordinate(31.895839f, -97.789573f),
                new Coordinate(32.895839f, -101.789573f),
                new Coordinate(34.115839f, -100.225732f),
                new Coordinate(32.335839f, -99.992232f),
                new Coordinate(33.535339f, -94.792232f),
                new Coordinate(32.234235f, -100.222222f),
            };

            return coordinates;
        }
    }
}
