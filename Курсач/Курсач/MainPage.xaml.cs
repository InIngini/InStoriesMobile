using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Курсач
{
	public partial class MainPage : ContentPage
	{
        //наши дорогие ширина и высота, теперь от них зависит все!
        double screenWidth;
        double screenHeight;

        public MainPage()
		{
            var metrics = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            screenWidth = metrics.Width / metrics.Density;
            screenHeight = metrics.Height / metrics.Density;


            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //if (true)
            //    Button_Clicked(button, EventArgs.Empty);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

    }
}
