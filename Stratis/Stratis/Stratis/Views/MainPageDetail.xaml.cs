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
    public partial class MainPageDetail : ContentPage
    {
        private readonly INavigationService accountService;
        public MainPageDetail(INavigationService accountServiceSettings)
        {
            this.accountService = accountServiceSettings;
            InitializeComponent();
            accountService.NavigateToAsync<StartViewModel>();

        }
        public MainPageDetail()
        {
            InitializeComponent();
        }
    }
}