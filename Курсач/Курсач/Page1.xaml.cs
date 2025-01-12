using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Курсач.Core.Data.Entities;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Errors;
using Курсач.Core.Interfaces;

namespace Курсач
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        private IServiceProvider ServiceProvider { get; set; }
        private IBookService BookService { get; set; }
        private IDatabaseManager DatabaseManager {  get; set; }

        private List<string> ButtonList;

        public Page1(IServiceProvider serviceProvider)
		{
            ServiceProvider = serviceProvider;
            BookService = ServiceProviderServiceExtensions.GetService<IBookService>(ServiceProvider);
            DatabaseManager = ServiceProviderServiceExtensions.GetService<IDatabaseManager>(ServiceProvider);

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            //1
            //аватар и ник
            User user = await DatabaseManager.GetUserAsync();
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
                Text = user.Login,
                FontFamily = "Istok Web",
                FontSize = 20,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            if (nameLabel.Text.Length > 12)
            {
                nameLabel.Text = nameLabel.Text.Substring(0, 12) + "...";
            }

            Bar.Children.Add(avatarImage, 0, 0);
            Bar.Children.Add(nameLabel, 1, 0);

            //2
            //книжки
            int id = user.Id;
            var book = new List<Book>();

            try
            {
                book = await BookService.GetAllBooksForUser(id);
            }
            catch (Exception ex)
            {
                var errorMessage = await ErrorsDeserialization.Deserialization(ex);
                await DisplayAlert("Ошибка", errorMessage, "OK");
            }

            ButtonList = book.Select(x => x.NameBook).ToList();

            int rowCount = ButtonList.Count / 2 + 1; // Определить количество строк

            for (int i = 0; i < rowCount; i++)
            {
                buttonsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(157) });
            }

            for (int i = 0; i < ButtonList.Count; i++)
            {
                var button = new Button
                {
                    AutomationId = ButtonList[i],
                    Text = ButtonList[i],
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
                    Margin = new Thickness(2, 0, 2, 0),
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
                buttonsGrid.Children.Add(stackLayout, i % 2, i / 2);
            }
        }

            private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            string buttonText = button.AutomationId;  // Получение текста кнопки

            await Navigation.PushAsync(new Page2(ServiceProvider, buttonText));  // Передача текста кнопки в конструктор Page2
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2(ServiceProvider, "Новая книга"));  // Передача текста кнопки в конструктор Page2
        }
    }
}