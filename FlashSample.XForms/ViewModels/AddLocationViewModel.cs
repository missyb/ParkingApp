using System;
using Acr.UserDialogs;
using FlashSample.SharedLibrary;
using Xamarin.Forms;

namespace FlashSample.XForms.ViewModels
{
    public class AddLocationViewModel : BaseViewModel
    {
        private string _name;
        private string _maxCapacityStr;
        private int _maxCapacity;

        public AddLocationViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string MaxCapacity
        {
            get => _maxCapacityStr;
            set => SetProperty(ref _maxCapacityStr, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {

            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var valid = ValidateSave();
            if(!valid)
            {
                await UserDialogs.Instance.AlertAsync("Validation failed");
                return;
            }

            var location = new ParkingLocation()
            {
                Name = Name,
                MaxCapacity = _maxCapacity
            };
            var result = await RestHelper.AddParkingLocationAsync(location);

            if(result)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Saving location failed");
            }
        }

        private bool ValidateSave()
        {
            var canSave = !String.IsNullOrWhiteSpace(_name)
                && !String.IsNullOrWhiteSpace(_maxCapacityStr)
                && int.TryParse(_maxCapacityStr, out _maxCapacity);

            return canSave;
        }
    }
}
