using FlashSample.SharedLibrary;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlashSample.XForms.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class LocationDetailViewModel : BaseViewModel
    {
        private string itemId;
        private ParkingLocation _location;

        public Command<ParkingSpot> SpotTapped { get; }

        public LocationDetailViewModel()
        {
            Spots = new ObservableCollection<ParkingSpot>();

            SpotTapped = new Command<ParkingSpot>(OnSpotSelected);
        }

        public string Id { get; set; }

        public string Name => _location?.Name;

        public string SpotsAvailable => _location != null ?
            string.Format("{0}/{1}", _location.MaxCapacity - Spots.Count(t => t.Occupied), _location.MaxCapacity) :
            string.Empty;

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                int id;
                if(int.TryParse(value, out id))
                {
                    LoadLocation(id);
                }
            }
        }

        public ObservableCollection<ParkingSpot> Spots { get; }

        private async void LoadLocation(int id)
        {
            Spots.Clear();

            var location = await RestHelper.GetParkingLocationAsync(id);
            var spots = await RestHelper.GetParkingSpotsAsync(id);

            if (location != null && spots != null)
            {
                _location = location;
                foreach(var spot in spots)
                {
                    Spots.Add(spot);
                }

                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(SpotsAvailable));
            }
            else
            {
                UserDialogs.Instance.Alert("Getting Location Failed");
            }
        }

        private async void OnSpotSelected(ParkingSpot spot)
        {
            if (spot == null)
                return;

            bool answer;
            if(spot.Occupied)
            {
                answer = await UserDialogs.Instance.ConfirmAsync("Do you want to remove the vehicle from this spot?");
            }
            else
            {
                answer = await UserDialogs.Instance.ConfirmAsync("Do you want to add a vehicle to this spot?");
            }

            if(answer)
            {
                UpdateSpot(spot, !spot.Occupied);
            }
        }

        private async void UpdateSpot(ParkingSpot spot, bool occupied)
        {
            spot.Occupied = occupied;
            var result = await RestHelper.UpdateSpotAsync(spot);
            if(result)
            {
                var spots = await RestHelper.GetParkingSpotsAsync(spot.ParkingLocationID);

                if (spots != null)
                {
                    Spots.Clear();
                    foreach (var s in spots)
                    {
                        Spots.Add(s);
                    }
                }

                OnPropertyChanged(nameof(SpotsAvailable));
            }
            else
            {
                UserDialogs.Instance.Alert(string.Format("Error {0} a vehicle", occupied ? "adding" : "removing"));
            }
        }
    }
}
