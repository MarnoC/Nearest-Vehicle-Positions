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
            // This is a simplified version; you might need a more sophisticated approach for a real-world application
            int cellSize = 1; // Adjust the cell size based on your data distribution
            long cellX = (long)(latitude / cellSize);
            long cellY = (long)(longitude / cellSize);
            return (cellX << 32) | cellY;
        }

        private double CalculateDistance(Coordinate coord1, VehiclePosition position)
        {
            // Use the Equirectangular approximation for distance calculation
            double x = (position.Longitude - coord1.Longitude) * Math.Cos((coord1.Latitude + position.Latitude) / 2);
            double y = position.Latitude - coord1.Latitude;

            // Radius of the Earth (in the desired unit)
            double radius = 6371; // Kilometers

            return Math.Sqrt(x * x + y * y) * radius;
        }
    }
}
