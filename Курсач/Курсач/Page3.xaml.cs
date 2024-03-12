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
    public partial class Page3 : ContentPage
    {
        string nameBook;
        public Page3(string nameBook)
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
            if (nameLabel.Text.Length > 13)
            {
                nameLabel.Text = nameLabel.Text.Substring(0, 13) + "...";
            }

            Bar.Children.Add(nameLabel, 1, 0);


            int i = 0;
            List<string> buttonTexts = new List<string> { "Текст 1", "Текст 2", "Текст 3", "Имя персонажа","Имя персонажа длиииинное"};
            foreach (string text in buttonTexts)
            {
                Image button = new Image
                {
                    BackgroundColor = Color.Transparent,
                    Margin = new Thickness(12, 0, 0, 0),
                    Aspect = Aspect.AspectFit,
                    Source = "avatar.png"
                };

                Label label = new Label
                {
                    Text = text,
                    TextColor = Color.White,
                    FontSize = 25,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontFamily = "Istok Web Bold", 
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(15, 3, 0, 0),
                };
                if(text.Length > 12)
                {
                    label.Text = label.Text.Substring(0, 12) + "...";
                }
                Image checkmark = new Image
                {
                    Source = "galochka.png",
                    Rotation = 180,
                    Margin = new Thickness(0, 0, 20, 0),
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                Button stackedButton = new Button
                {
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, 0,0,0),
                    BackgroundColor = Color.FromHex("#B280FE"),
                    CornerRadius = 12,
                    HeightRequest = 66,
                    
                };
                StackLayout buttonContent = new StackLayout
                {
                    Margin = new Thickness(18,-60,0,0),
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Children = { button, label, checkmark },
                    
                };
                Button stackedButton2 = new Button
                {
                    Padding = new Thickness(0),
                    Margin = new Thickness(0, -60, 0, 0),
                    BackgroundColor = Color.Transparent,
                    CornerRadius = 12,
                    HeightRequest = 66,
                    AutomationId = text,

                };
                stackedButton2.Clicked += Button3_1_Clicked;

                StackLayout mainStackLayout = new StackLayout
                {
                    Margin = new Thickness(12,0),
                    AutomationId = text,
                    Children = {  stackedButton, buttonContent, stackedButton2 }
                };

                buttonsGrid.RowSpacing = 30;
                buttonsGrid.Children.Add(mainStackLayout,0,i);
                i++;
            }

        }

        private async void Button3_1_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            string namePerson = button.AutomationId;  // Получение текста кнопки

            await Navigation.PushAsync(new Page3_1(nameBook,namePerson,"Личность"));
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
    
