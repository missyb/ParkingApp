using Xamarin.Forms;
using FlashSample.XForms.ViewModels;
using FlashSample.SharedLibrary;

namespace FlashSample.XForms.Views
{
    public partial class AddLocationPage : ContentPage
    {
        public ParkingLocation Item { get; set; }

        public AddLocationPage()
        {
            InitializeComponent();
            BindingContext = new AddLocationViewModel();
        }
    }
}
