using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Курсач.Core.Data.DTO;
using Курсач.Core.DB;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Errors;
using Курсач.Core.Interfaces;

namespace Курсач
{
    public partial class MainPage : ContentPage
	{
        private IServiceProvider ServiceProvider { get; set; }
        private IUserService UserService { get; set; }

        //наши дорогие ширина и высота, теперь от них зависит все!
        private double screenWidth;
        private double screenHeight;

        public MainPage(IServiceProvider serviceProvider)
		{
            ServiceProvider = serviceProvider;
            UserService = ServiceProviderServiceExtensions.GetService<IUserService>(ServiceProvider);

            var metrics = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            screenWidth = metrics.Width / metrics.Density;
            screenHeight = metrics.Height / metrics.Density;

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            if (true)
                Button_Clicked(button, EventArgs.Empty);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var loginData = new LoginData
            {
                Login = loginEntry.Text ?? "username", // Надо не забыть поменять на пустые строки
                Password = passwordEntry.Text ?? "password"
            };
            
            try
            {
                var token = await UserService.LoginUser(loginData);
                await Navigation.PushAsync(new Page1(ServiceProvider));
            }
            catch (Exception ex)
            {
                var errorMessage = await ErrorsDeserialization.Deserialization(ex);
                await DisplayAlert("Ошибка", errorMessage, "OK");
            }
        }

    }
}
