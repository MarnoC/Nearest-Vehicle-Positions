using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nearest_Vehicle_Positions
{
    public static class BinaryReaderExtensions
    {
        public static string ReadNullTerminatedString(this BinaryReader reader)
        {
            List<byte> bytes = new List<byte>();
            byte b;
            while ((b = reader.ReadByte()) != 0)
            {
                bytes.Add(b);
            }
            return Encoding.ASCII.GetString(bytes.ToArray());
        }
    }
}
