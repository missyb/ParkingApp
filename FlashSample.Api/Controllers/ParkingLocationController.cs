using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlashSample.SharedLibrary;
using FlashSample.Api.Database;

namespace FlashSample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLocationController : ControllerBase
    {
        private readonly ILogger<ParkingLocationController> _logger;
        private readonly IDatabase _database;

        public ParkingLocationController(IDatabase database, ILogger<ParkingLocationController> logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ParkingLocation>> GetAll()
        {
            IEnumerable<ParkingLocation> items = null;

            try
            {
                items = await _database.GetItemsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return items;
        }

        [HttpGet("spots/{id}")]
        public async Task<IEnumerable<ParkingSpot>> GetSpots(int id)
        {
            IEnumerable<ParkingSpot> items = null;

            try
            {
                items = await _database.GetParkingSpotsAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ParkingLocation> Get(int id)
        {
            ParkingLocation item = null;

            try
            {
                item = await _database.GetItemAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return item;
        }

        [HttpPut("spot")]
        public async Task<bool> UpdateSpot(ParkingSpot spot)
        {
            var result = false;
            try
            {
                var currentSpot = await _database.GetSpotAsync(spot.ID);
                if(currentSpot.Occupied == spot.Occupied)
                {
                    result = false;
                }
                else
                {
                    await _database.UpdateItemAsync(spot);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

            return result;
        }

        [HttpPost]
        public async Task<int> AddParkingLocation(ParkingLocation location)
        {
            int id = 0;
            try
            {
                id = await _database.AddItemAsync(location);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return 0;
            }

            return id;
        }

        [HttpDelete]
        public bool DeleteAllLocations()
        {
            try
            {
                _database.ClearDatabaseAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return false;
            }

            return true;
        }
    }
}
