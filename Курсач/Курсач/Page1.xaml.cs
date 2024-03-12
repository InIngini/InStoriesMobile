using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Курсач
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        List<string> buttonList;

        public Page1 ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);


            //1
            //аватар и ник
            var avatarImage = new ImageButton
            {
                Source = "avatar.png",
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                WidthRequest = 40,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var nameLabel = new Label
            {
                Text = "Имя пользователя",
                FontFamily = "Istok Web",
                FontSize = 20,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            if (nameLabel.Text.Length > 12)
            {
                nameLabel.Text = nameLabel.Text.Substring(0,12) + "...";
            }

            Bar.Children.Add(avatarImage,0,0);
            Bar.Children.Add(nameLabel,1,0);

            //2
            //книжки
            buttonList = new List<string> { "Book's name", "Book's long name", "Book's very long name"}; // Ваш список строк для текста кнопок

            int rowCount = buttonList.Count / 2 + 1; // Определить количество строк

            for (int i = 0; i < rowCount; i++)
            {
                buttonsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(157) });
            }

            for (int i = 0; i < buttonList.Count; i++)
            {
                var button = new Button
                {
                    AutomationId = buttonList[i],
                    Text = buttonList[i],
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
                    Margin = new Thickness(2,0,2,0),
                    
                };
                button.Clicked += Button_Clicked;

                if (button.Text.Length > 18)
                {
                    button.Text = button.Text.Substring(0, 18) + "...";
                }

                // Создание прямоугольника под каждой кнопкой
                var rectangle = new BoxView
                {
                    BackgroundColor = Color.FromHex("#B280FE"),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    HeightRequest = button.HeightRequest,
                    WidthRequest = button.WidthRequest,
                    CornerRadius = 4,
                    TranslationY = -button.HeightRequest, // Сдвигаем прямоугольник вверх, чтобы кнопка перекрывала его
                    TranslationX = -10
                };

                var stackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center
                };
                stackLayout.Children.Add(button);
                stackLayout.Children.Add(rectangle);

                buttonsGrid.RowSpacing = 44;

                //buttonsGrid.RowSpacing = 44; // Устанавливаем расстояние между строками в buttonsGrid
                buttonsGrid.Children.Add(stackLayout, i % 2, i / 2);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            string buttonText = button.AutomationId;  // Получение текста кнопки

            await Navigation.PushAsync(new Page2(buttonText));  // Передача текста кнопки в конструктор Page2
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2("Новая книга"));  // Передача текста кнопки в конструктор Page2
        }
    }
}