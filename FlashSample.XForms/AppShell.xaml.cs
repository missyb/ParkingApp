using FlashSample.XForms.Views;
using Xamarin.Forms;

namespace FlashSample.XForms
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LocationDetailPage), typeof(LocationDetailPage));
            Routing.RegisterRoute(nameof(AddLocationPage), typeof(AddLocationPage));
        }

    }
}
