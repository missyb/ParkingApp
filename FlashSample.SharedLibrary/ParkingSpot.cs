using System;
namespace FlashSample.SharedLibrary
{
    public class ParkingSpot
    {
        public int ID { get; set; }

        public int ParkingLocationID { get; set; }

        public int SpotNumber { get; set; }

        public bool Occupied { get; set; }
    }
}
