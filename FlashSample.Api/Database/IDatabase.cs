using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlashSample.SharedLibrary;

namespace FlashSample.Api.Database
{
    public interface IDatabase
    {
        Task<IEnumerable<ParkingLocation>> GetItemsAsync();
        Task<ParkingLocation> GetItemAsync(int id);
        Task<ParkingSpot> GetSpotAsync(int id);
        Task<int> UpdateItemAsync(ParkingLocation item);
        Task<int> AddItemAsync(ParkingLocation item);
        Task<IEnumerable<ParkingSpot>> GetParkingSpotsAsync(int parkingLocationID);
        Task<int> UpdateItemAsync(ParkingSpot item);
        Task ClearDatabaseAsync();
        Task<int> GetOccupiedParkingSpotsCountAsync(int parkingLocationID);
    }
}
