using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePosition_Application
{
    public class VehiclePosition
    {
        public int VehicleId { get; }
        public string Registration { get; }
        public float Latitude { get; }
        public float Longitude { get; }
        public ulong RecordedTimeUTC { get; }

        public VehiclePosition(int vehicleId, string registration, float latitude, float longitude, ulong recordedTimeUTC)
        {
            VehicleId = vehicleId;
            Registration = registration;
            Latitude = latitude;
            Longitude = longitude;
            RecordedTimeUTC = recordedTimeUTC;
        }
    }
}
