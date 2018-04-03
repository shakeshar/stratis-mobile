using Stratis.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stratis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;
        public MainViewPageMasterViewModel Viewmodel;
        public MainPageMaster()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                Icon = "slideout.png";
           
            Viewmodel = new MainViewPageMasterViewModel();
            BindingContext = Viewmodel;
            ListView = MenuItemsListView;
        }

       
    }
}