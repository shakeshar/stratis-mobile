using System;
using System.Threading.Tasks;
using Stratis.Domain;
using Stratis.Models;
using Stratis.Services;
using Stratis.Views;
using Xamarin.Forms;

namespace Stratis
{
	public partial class App : Application
	{
        public static Strat Account { get; set; }

        public App ()
		{
			InitializeComponent();
            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
        }

		protected override async void OnStart ()
		{
            // Handle when your app starts
            base.OnStart();
            if (Device.RuntimePlatform != Device.UWP)
            {

                await InitNavigation();
            }// Handle when your app starts
        }

		protected override async void OnSleep ()
		{
            string contacts = "contacts";
            if (Properties.ContainsKey(contacts))
            {
                var x= Properties[contacts] as DataStore<Contact>;
                Properties[contacts] = Newtonsoft.Json.JsonConvert.SerializeObject(x);

            }
            await SavePropertiesAsync();
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            
			// Handle when your app resumes
		}
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();           
            return navigationService.InitializeAsync();
        }
    }
}
