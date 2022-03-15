using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FlashSample.SharedLibrary;
using FlashSample.XForms.Views;

namespace FlashSample.XForms.ViewModels
{
    public class LocationsViewModel : BaseViewModel
    {
        private ParkingLocation _selectedLocation;

        public ObservableCollection<ParkingLocation> Locations { get; }
        public Command LoadLocationsCommand { get; }
        public Command<ParkingLocation> LocationTapped { get; }

        public LocationsViewModel()
        {
            Title = "Parking Locations";
            Locations = new ObservableCollection<ParkingLocation>();
            LoadLocationsCommand = new Command(async () => await ExecuteLoadLocationsCommand());

            LocationTapped = new Command<ParkingLocation>(OnLocationSelected);
        }

        async Task ExecuteLoadLocationsCommand()
        {
            IsBusy = true;

            try
            {
                await PopulateLocationDataAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedLocation = null;
        }

        public ParkingLocation SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                SetProperty(ref _selectedLocation, value);
                OnLocationSelected(value);
            }
        }

        async void OnLocationSelected(ParkingLocation location)
        {
            if (location == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LocationDetailPage)}?{nameof(LocationDetailViewModel.ItemId)}={location.ID.ToString()}");
        }

        private async Task PopulateLocationDataAsync()
        {
            Locations.Clear();

            var locations = await RestHelper.GetParkingLocationsAsync();
            foreach(var location in locations)
            {
                Locations.Add(location);
            }
        }
    }
}
