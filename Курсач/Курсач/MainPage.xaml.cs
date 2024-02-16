using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Курсач
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            if (true)
                Button_Clicked(button, EventArgs.Empty);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());

        }

    }
}
