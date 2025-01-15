using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Курсач.Core.DB;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Services.Interfaces;

namespace Курсач
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IBookService BookService { get; set; }
        private int BookId;
        public Page3(IServiceProvider serviceProvider, int id)
        {
            ServiceProvider = serviceProvider;
            BookService = ServiceProviderServiceExtensions.GetService<IBookService>(ServiceProvider);

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BookId = id;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var nameBook = (await BookService.GetBook(BookId)).NameBook;
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
                Image avatarImage = new Image
                {
                    BackgroundColor = Color.Transparent,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFit,
                    Source = "avatar.png"
                };

                Label namePersonLabel = new Label
                {
                    Text = text,
                    TextColor = Color.White,
                    FontSize = 25,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontFamily = "Istok Web Bold", 
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                };
                if(text.Length > 12)
                {
                    namePersonLabel.Text = namePersonLabel.Text.Substring(0, 12) + "...";
                }
                Image checkmark = new Image
                {
                    Source = "galochka.png",
                    Rotation = 180,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                };
                Grid buttonContent = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    }
                };
                buttonContent.Children.Add(avatarImage, 0, 0); // Первая колонка, первая строка
                buttonContent.Children.Add(namePersonLabel, 1, 0); // Вторая колонка, первая строка
                buttonContent.Children.Add(checkmark, 2, 0); // Третья колонка, первая строка
                
                Button stackedButton = new Button
                {
                    Padding = new Thickness(0),
                    BackgroundColor = Color.FromHex("#B280FE"),
                    CornerRadius = 12,
                    HeightRequest = 66,
                    
                };
                Button stackedButton2 = new Button
                {
                    Padding = new Thickness(0),
                    BackgroundColor = Color.Transparent,
                    CornerRadius = 12,
                    HeightRequest = 66,
                    AutomationId = text,

                };
                stackedButton2.Clicked += Button3_1_Clicked;

                buttonsGrid.RowSpacing = 20;
                buttonsGrid.Children.Add(stackedButton,0,i);
                buttonsGrid.Children.Add(buttonContent,0,i);
                buttonsGrid.Children.Add(stackedButton2,0,i);

                i++;
            }

        }

        private async void Button3_1_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton)
            {
                await Navigation.PushAsync(new Page3_1(ServiceProvider, BookId, "", "Личность"));
            }
            else
            {
                Button button = (Button)sender;  // Получение объекта Button, который отправил событие
                string namePerson = button.AutomationId;  // Получение текста кнопки

                await Navigation.PushAsync(new Page3_1(ServiceProvider, BookId, namePerson, "Личность"));
            }

        }

        private async void ButtonHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2(ServiceProvider, BookId));
        }
        private async void ButtonPersona_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3(ServiceProvider, BookId));
        }
        private async void ButtonShema_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page4(ServiceProvider, BookId));
        }
        private async void ButtonTime_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page5(ServiceProvider, BookId));
        }
    }
}
    
