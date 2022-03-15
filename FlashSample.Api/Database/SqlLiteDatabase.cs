using System.Collections.Generic;
using System.Threading.Tasks;
using FlashSample.SharedLibrary;
using SQLite;
using System.Linq;
using System;
using System.IO;

namespace FlashSample.Api.Database
{
    public class SqlLiteDatabase : IDatabase
    {
        private const string DBNAME = "SQLite.db3";
        private const SQLite.SQLiteOpenFlags FLAGS = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

        private SQLiteAsyncConnection _connection;

        public SqlLiteDatabase()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(basePath, DBNAME);

            _connection = new SQLiteAsyncConnection(dbPath, FLAGS);
            _connection.CreateTableAsync<PARKINGLOCATION>();
            _connection.CreateTableAsync<PARKINGSPOT>();
        }

        public async Task<IEnumerable<ParkingLocation>> GetItemsAsync()
        {
            var items = await _connection.Table<PARKINGLOCATION>().ToArrayAsync();
            return items?.Select(x => x.ToParkingLocation());
        }

        public async Task<ParkingLocation> GetItemAsync(int id)
        {
            var item = await _connection.Table<PARKINGLOCATION>().Where(i => i.ID == id).FirstOrDefaultAsync();
            return item?.ToParkingLocation();
        }

        public async Task<ParkingSpot> GetSpotAsync(int id)
        {
            var item = await _connection.Table<PARKINGSPOT>().Where(i => i.ID == id).FirstOrDefaultAsync();
            return item?.ToParkingSpot();
        }

        public async Task<int> UpdateItemAsync(ParkingLocation item)
        {
            return await _connection.UpdateAsync(item.FromParkingLocation());
        }

        public async Task<int> AddItemAsync(ParkingLocation item)
        {
            var id = await _connection.InsertAsync(item.FromParkingLocation());
            var max = await _connection.Table<PARKINGLOCATION>().OrderByDescending(p => p.ID).FirstAsync();
            for (int i = 0; i < item.MaxCapacity; i++)
            {
                await AddItemAsync(new ParkingSpot
                {
                    Occupied = false,
                    ParkingLocationID = max.ID,
                    SpotNumber = i + 1
                });
            }

            return id;
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotsAsync(int parkingLocationID)
        {
            var items = await _connection.Table<PARKINGSPOT>().Where(t => t.PARKINGLOCATIONID == parkingLocationID).ToArrayAsync();
            return items?.Select(x => x.ToParkingSpot());
        }

        public async Task<int> GetOccupiedParkingSpotsCountAsync(int parkingLocationID)
        {
            return await _connection.Table<PARKINGSPOT>().CountAsync(t => t.PARKINGLOCATIONID == parkingLocationID && t.OCCUPIED);
        }

        public async Task<int> UpdateItemAsync(ParkingSpot item)
        {
            return await _connection.UpdateAsync(item.FromParkingSpot());
        }

        public async Task ClearDatabaseAsync()
        {
            await _connection.DeleteAllAsync<PARKINGLOCATION>();
            await _connection.DeleteAllAsync<PARKINGSPOT>();
        }

        private async Task<int> AddItemAsync(ParkingSpot item)
        {
            return await _connection.InsertAsync(item.FromParkingSpot());
        }
    }
}