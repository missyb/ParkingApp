using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlashSample.SharedLibrary;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FlashSample.XForms.Services
{
    public class RestHelper : IRestHelper
    {
        private readonly string _baseUrl;

        public RestHelper()
        {
            var urlService = DependencyService.Get<ILocalHostUrlService>();
            _baseUrl = urlService.LocalHostUrl;
        }

        public async Task<List<ParkingLocation>> GetParkingLocationsAsync()
        {
            var result = new List<ParkingLocation>();
            using (var client = GetClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("/parkinglocation/all");

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<ParkingLocation>>(content);
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public async Task<List<ParkingSpot>> GetParkingSpotsAsync(int locationId)
        {
            var result = new List<ParkingSpot>();
            using (var client = GetClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format("/parkinglocation/spots/{0}", locationId));

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<ParkingSpot>>(content);
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public async Task<ParkingLocation> GetParkingLocationAsync(int id)
        {
            ParkingLocation result = null;
            using (var client = GetClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format("/parkinglocation/{0}", id));

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ParkingLocation>(content);
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public async Task<bool> AddParkingLocationAsync(ParkingLocation location)
        {
            bool result = false;
            using (var client = GetClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(location), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("/parkinglocation", content);

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public async Task<bool> UpdateSpotAsync(ParkingSpot spot)
        {
            bool result = false;
            using (var client = GetClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(spot), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(string.Format("/parkinglocation/spot"), content);

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public async Task<bool> ClearDatabaseAsync()
        {
            var result = false;
            using (var client = GetClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync("/parkinglocation");

                    if (response.IsSuccessStatusCode)
                    {
                        result = true;
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        private HttpClient GetClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
