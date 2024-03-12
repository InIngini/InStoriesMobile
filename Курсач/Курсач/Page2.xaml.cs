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
    public partial class Page2 : ContentPage
    {
        string nameBook;
        public Page2(string nameBook)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.nameBook = nameBook;

            //название книги
            var nameLabel = new Label
            {
                Text = nameBook,
                FontFamily = "Istok Web",
                FontSize = 20,
                TextColor = Color.White,
                Margin = new Thickness(-5, 2, 0, 2),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            if (nameLabel.Text.Length > 12)
            {
                nameLabel.Text = nameLabel.Text.Substring(0, 12) + "...";
            }

            Bar.Children.Add(nameLabel, 1, 0);

            //книжка и название

            Label textLabel = new Label
            {
                Text = "Название",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Istok Web Bold",
                TextColor = Color.White,
                Margin = new Thickness(2, 0, 0, 0)
            };
            Editor textEditor = new Editor
            {
                WidthRequest = 100,
                HeightRequest = 100,
                BackgroundColor = Color.Transparent,
                Text = nameBook,
                FontSize = 20,
                FontFamily = "Istok Web",
                TextColor = Color.White
            };

            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(textEditor);

            //book
            // Создание прямоугольника под каждой кнопкой
            var rectangle = new BoxView
            {
                BackgroundColor = Color.FromHex("#B280FE"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 157,
                WidthRequest = 123,
                CornerRadius = 4,
                Margin = new Thickness(2, 0, 2, 0)

            };
            var button = new Button
            {
                ImageSource = "photo.png",
                FontSize = 20,
                FontFamily = "Istok Web",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#161519"),
                BorderColor = Color.FromHex("#B280FE"),
                BorderWidth = 1,
                CornerRadius = 4,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 157,
                WidthRequest = 123,
                TranslationY = -rectangle.HeightRequest-10, // Сдвигаем прямоугольник вверх, чтобы кнопка перекрывала его
                TranslationX = 10

            };
            //button.Clicked += Button_Clicked;

            var stackLayout2 = new StackLayout();
            stackLayout2.Children.Add(rectangle);
            stackLayout2.Children.Add(button);
            

            buttonsGrid.Children.Add(stackLayout2, 0, 0);
            buttonsGrid.Children.Add(stackLayout, 1, 0);

        }
        private async void Button1_Clicked(object sender, EventArgs e)
        {
            //тут надо написать, что ничего не сохранится
            await Navigation.PushAsync(new Page1());

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