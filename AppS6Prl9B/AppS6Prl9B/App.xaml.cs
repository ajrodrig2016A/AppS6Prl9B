using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppS6Prl9B
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new viewInsertarB();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
