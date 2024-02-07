using Nearest_Vehicle_Positions;
using System.Text;

namespace VehiclePosition_Application
{
    public class SpatialIndex
    {
        private Dictionary<long, List<VehiclePosition>> spatialGrid;

        public SpatialIndex(List<VehiclePosition> positions)
        {
            spatialGrid = new Dictionary<long, List<VehiclePosition>>();

            foreach (var position in positions)
            {
                long cellKey = ComputeCellKey(position.Latitude, position.Longitude);
                if (!spatialGrid.TryGetValue(cellKey, out var cellPositions))
                {
                    cellPositions = new List<VehiclePosition>();
                    spatialGrid[cellKey] = cellPositions;
                }
                cellPositions.Add(position);
            }
        }

        public int FindNearestPositionId(Coordinate coordinate)
        {
            long cellKey = ComputeCellKey(coordinate.Latitude, coordinate.Longitude);
            if (!spatialGrid.TryGetValue(cellKey, out var cellPositions))
            {
                return -1; // No positions in the cell
            }

            int nearestPositionId = -1;
            double minDistance = double.MaxValue;

            foreach (var position in cellPositions)
            {
                double distance = CalculateDistance(coordinate, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPositionId = position.VehicleId;
                }
            }

            return nearestPositionId;
        }

        private long ComputeCellKey(double latitude, double longitude)
        {
            // Adjust the cell size based on your data distribution
            double cellSize = 1.0;
            double earthRadius = 6371.0; // Earth's radius in kilometers

            // Convert latitude and longitude to radians
            double latRad = latitude * (Math.PI / 180.0);
            double lonRad = longitude * (Math.PI / 180.0);

            // Calculate the horizontal and vertical distance in meters
            double dx = cellSize * earthRadius * Math.Cos(latRad) * Math.Cos(lonRad);
            double dy = cellSize * earthRadius * Math.Cos(latRad) * Math.Sin(lonRad);

            // Calculate cell indices
            long cellX = (long)Math.Floor(dx);
            long cellY = (long)Math.Floor(dy);

            // Combine cell indices into a single key
            return (cellX << 32) | cellY;
        }

        private double CalculateDistance(Coordinate coord1, VehiclePosition position)
        {
            // Equirectangular approximation
            double lat1 = ToRadians(coord1.Latitude);
            double lon1 = ToRadians(coord1.Longitude);
            double lat2 = ToRadians(position.Latitude);
            double lon2 = ToRadians(position.Longitude);

            double x = (lon2 - lon1) * Math.Cos((lat1 + lat2) / 2);
            double y = lat2 - lat1;

            // Radius of the Earth (in the desired unit)
            double radius = 6371; // Kilometers

            return Math.Sqrt(x * x + y * y) * radius;
        }

        private double ToRadians(double degree)
        {
            return degree * (Math.PI / 180);
        }
    }
}
