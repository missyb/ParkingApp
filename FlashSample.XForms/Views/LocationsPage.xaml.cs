using Xamarin.Forms;
using FlashSample.XForms.ViewModels;

namespace FlashSample.XForms.Views
{
    public partial class LocationsPage : ContentPage
    {
        LocationsViewModel _viewModel;

        public LocationsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LocationsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
