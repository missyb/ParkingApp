using System;
using FlashSample.Api.Database;
using FlashSample.SharedLibrary;

namespace FlashSample.Api.Database
{
    public static class ObjectMappingExtension
    {
        public static PARKINGLOCATION FromParkingLocation(this ParkingLocation parkingLocation)
        {
            return new PARKINGLOCATION()
            {
                ID = parkingLocation.ID,
                NAME = parkingLocation.Name,
                MAXCAPACITY = parkingLocation.MaxCapacity
            };
        }

        public static ParkingLocation ToParkingLocation(this PARKINGLOCATION parkingLocation)
        {
            return new ParkingLocation()
            {
                ID = parkingLocation.ID,
                Name = parkingLocation.NAME,
                MaxCapacity = parkingLocation.MAXCAPACITY
            };
        }

        public static ParkingSpot ToParkingSpot(this PARKINGSPOT parkingSpot)
        {
            return new ParkingSpot()
            {
                ID = parkingSpot.ID,
                ParkingLocationID = parkingSpot.PARKINGLOCATIONID,
                Occupied = parkingSpot.OCCUPIED,
                SpotNumber = parkingSpot.SPOTNUMBER
            };
        }

        public static PARKINGSPOT FromParkingSpot(this ParkingSpot parkingSpot)
        {
            return new PARKINGSPOT()
            {
                ID = parkingSpot.ID,
                PARKINGLOCATIONID = parkingSpot.ParkingLocationID,
                OCCUPIED = parkingSpot.Occupied,
                SPOTNUMBER = parkingSpot.SpotNumber
            };

        }
    }
}
