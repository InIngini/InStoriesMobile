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
        string nameBook;
        public Page5(string nameBook)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.nameBook = nameBook;
            

        }

        private async void Button5_1_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            string buttonText = button.AutomationId;  // Получение текста кнопки

            await Navigation.PushAsync(new Page3_1(nameBook, buttonText, "Личность"));
        }

        private async void ButtonHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2(nameBook));
        }
        private async void ButtonPersona_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3(nameBook));
        }
        private async void ButtonShema_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page4(nameBook));
        }
        private async void ButtonTime_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page5(nameBook));
        }
    }
}