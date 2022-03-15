using Acr.UserDialogs;
using FlashSample.XForms.Views;
using Xamarin.Forms;

namespace FlashSample.XForms.ViewModels
{
    public class ToolsViewModel : BaseViewModel
    {
        public Command AddLocationCommand { get; }
        public Command ClearDatabaseCommand { get; }

        public ToolsViewModel()
        {
            Title = "Tools";

            AddLocationCommand = new Command(OnAddLocation);
            ClearDatabaseCommand = new Command(OnClearDatabase);
        }

        private async void OnAddLocation()
        {
            await Shell.Current.GoToAsync(nameof(AddLocationPage));
        }

        private async void OnClearDatabase()
        {
            var result = await UserDialogs.Instance.ConfirmAsync("Are you sure want to clear the whole database?");
            if(result)
            {
                await RestHelper.ClearDatabaseAsync();
            }
        }
    }
}
