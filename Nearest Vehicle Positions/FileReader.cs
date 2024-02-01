using Nearest_Vehicle_Positions;

namespace VehiclePosition_Application
{
    internal class FileReader
    {
        public static int index = 0;

        internal static List<VehiclePosition> ReadDataFile()
        {
            string fileName = "VehiclePositions.dat";
            //Get the file path to the compiled directory + Data\VehiclePositions.dat 
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);

            //Check if file exists 
            if (File.Exists(path))
            {
                List<VehiclePosition> vehiclePositions = LoadData(path);
                return vehiclePositions;
            }
            else
            {
                Console.WriteLine("File does not exist at path: " + path);
                return null;
            }
        }

        public static List<VehiclePosition> LoadData(string binaryFilePath)
        {
            List<VehiclePosition> positions = new List<VehiclePosition>();

            using (BinaryReader reader = new BinaryReader(File.OpenRead(binaryFilePath)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int vehicleId = reader.ReadInt32();
                    string registration = reader.ReadNullTerminatedString();
                    float latitude = reader.ReadSingle();
                    float longitude = reader.ReadSingle();
                    ulong recordedTimeUTC = reader.ReadUInt64();

                    positions.Add(new VehiclePosition(vehicleId, registration, latitude, longitude, recordedTimeUTC));
                }
            }

            return positions;
        }
    }
}
