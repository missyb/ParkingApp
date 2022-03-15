using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlashSample.SharedLibrary;

namespace FlashSample.XForms.Services
{
    public interface IRestHelper
    {
        Task<List<ParkingLocation>> GetParkingLocationsAsync();
        Task<List<ParkingSpot>> GetParkingSpotsAsync(int locationId);
        Task<ParkingLocation> GetParkingLocationAsync(int id);
        Task<bool> AddParkingLocationAsync(ParkingLocation location);
        Task<bool> UpdateSpotAsync(ParkingSpot spot);
        Task<bool> ClearDatabaseAsync();
    }
}
