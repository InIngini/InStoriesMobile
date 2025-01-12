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
using Курсач.Core.DB;
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
        private IDatabaseSyncService DatabaseSyncService { get; set; }

        private List<string> ButtonList;

        public Page1(IServiceProvider serviceProvider)
		{
            ServiceProvider = serviceProvider;
            BookService = ServiceProviderServiceExtensions.GetService<IBookService>(ServiceProvider);
            DatabaseManager = ServiceProviderServiceExtensions.GetService<IDatabaseManager>(ServiceProvider);
            DatabaseSyncService = ServiceProviderServiceExtensions.GetService<IDatabaseSyncService>(ServiceProvider);

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

            await DatabaseSyncService.SyncDatabasesAsync(user.Id); // Синхронизация

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

            int rowCount = book.Count / 2 + 1; // Определить количество строк

            for (int i = 0; i < rowCount; i++)
            {
                buttonsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(157) });
            }

            for (int i = 0; i < book.Count; i++)
            {
                var rectangle = new BoxView
                {
                    BackgroundColor = Color.FromHex("#B280FE"),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    HeightRequest = 157, // Установим высоту, равную высоте кнопки
                    WidthRequest = 123,   // Установим ширину, равную ширине кнопки
                    CornerRadius = 4,
                    TranslationX = -10,
                    Margin = new Thickness(2, 10, 2, 0),
                };

                var button = new Button
                {
                    AutomationId = $"{book[i].Id}",
                    Text = book[i].NameBook,
                    FontSize = 20,
                    FontFamily = "Istok Web",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("#161519"),
                    BorderColor = Color.FromHex("#B280FE"),
                    BorderWidth = 1,
                    CornerRadius = 4,
                    VerticalOptions = LayoutOptions.Center, // Устанавливаем расположение кнопки
                    HorizontalOptions = LayoutOptions.Center, // Устанавливаем расположение кнопки
                    HeightRequest = 157,
                    WidthRequest = 123,
                    TranslationY = -rectangle.HeightRequest-15, // Сдвигаем кнопку вверх, чтобы перекрывать прямоугольник
                };
               
                button.Clicked += Button_Clicked;

                if (button.Text.Length > 18)
                {
                    button.Text = button.Text.Substring(0, 18) + "...";
                }

                var stackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center
                };
                stackLayout.Children.Add(rectangle);
                stackLayout.Children.Add(button);

                buttonsGrid.RowSpacing = 44;
                buttonsGrid.Children.Add(stackLayout, i % 2, i / 2);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            int id = Convert.ToInt32(button.AutomationId);  // Получение id кнопки

            await Navigation.PushAsync(new Page2(ServiceProvider, id));  // Передача текста кнопки в конструктор Page2
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2(ServiceProvider, 0));  // Передача текста кнопки в конструктор Page2
        }
    }
}