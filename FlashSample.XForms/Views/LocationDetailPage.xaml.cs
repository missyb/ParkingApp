using Xamarin.Forms;
using FlashSample.XForms.ViewModels;

namespace FlashSample.XForms.Views
{
    public partial class LocationDetailPage : ContentPage
    {
        public LocationDetailPage()
        {
            InitializeComponent();
            BindingContext = new LocationDetailViewModel();
        }
    }
}
