using Stratis.Services;
using Stratis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stratis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        protected readonly INavigationService NavigationService;
        public MainPage()
        {
            InitializeComponent();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            if (Device.RuntimePlatform == Device.UWP)
            {
                Icon = "slideout.png";
                MasterBehavior = MasterBehavior.Popover;

            }
        }
        protected override void OnAppearing()
        {
            Load();
        }
        public void Load()
        {
            MasterPage.ListView.SelectedItem = MasterPage.Viewmodel.MenuItems[0];
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainViewPageMenuItem;
            if (item == null)
                return;
            NavigationService.NavigateToAsync(item.TargetType, false);
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}