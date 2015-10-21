using PrivatePhotoStorageXamarin.ViewModels;
using Xamarin.Forms;

namespace PrivatePhotoStorageXamarin.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var _vm = new GalleryViewModel();
            BindingContext = _vm;
            
        }
    }
}
