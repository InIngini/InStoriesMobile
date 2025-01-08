using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Курсач
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page5 : ContentPage
    {
        private IServiceProvider ServiceProvider { get; set; }
        private string NameBook;
        public Page5(IServiceProvider serviceProvider, string nameBook)
        {
            ServiceProvider = serviceProvider;

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            NameBook = nameBook;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {

        }

        private async void Button5_1_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            string buttonText = button.AutomationId;  // Получение текста кнопки

            await Navigation.PushAsync(new Page3_1(ServiceProvider, NameBook, buttonText, "Личность"));
        }

        private async void ButtonHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2(ServiceProvider, NameBook));
        }
        private async void ButtonPersona_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3(ServiceProvider, NameBook));
        }
        private async void ButtonShema_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page4(ServiceProvider, NameBook));
        }
        private async void ButtonTime_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page5(ServiceProvider, NameBook));
        }
    }
}