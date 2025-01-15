using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Services.Interfaces;

namespace Курсач
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3_2 : ContentPage
    {
        private int BookId;
        private string NamePerson;
        private string Запись; 
        private IServiceProvider ServiceProvider { get; set; }
        private IBookService BookService { get; set; }
        public Page3_2(IServiceProvider serviceProvider, int id, string запись, string person)
        {
            ServiceProvider = serviceProvider;
            BookService = ServiceProviderServiceExtensions.GetService<IBookService>(ServiceProvider);

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BookId = id;
            NamePerson = person;
            Запись = запись;

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

            // Entry в первой строке
            Entry entry1 = new Entry
            {
                Text = Запись,
                TextColor = Color.White,
                FontFamily = "Istok Web",
                FontSize = 25
            };
            buttonsGrid.Children.Add(entry1, 0, 0); // Grid.Row="0"

            // Многострочный Entry во второй строке
            Editor editor = new Editor
            {
                Text = "Тут написано капец как много текста, чтобы просто проверить, как будет выглядеть",
                TextColor = Color.White,
                FontFamily = "Istok Web",
                FontSize = 20,
                HeightRequest = 579,
                IsTextPredictionEnabled = false,
                Keyboard = Keyboard.Chat,
                HorizontalOptions = LayoutOptions.FillAndExpand // Растянуть на всю строку
            };
            buttonsGrid.Children.Add(editor, 0, 1); // Grid.Row="1"

            //Entry в третьей строке
            Entry entry3 = new Entry
            {
                Placeholder = "дд.мм.гггг",
                PlaceholderColor = Color.White,
                TextColor = Color.White,
                FontFamily = "Istok Web",
                FontSize = 25,
                WidthRequest = 280
            };

            entry3.TextChanged += (sender, e) =>
            {
                try
                {
                    string text = entry3.Text;

                    // Удаление всех символов, кроме цифр,точки,тире и пробелов по бокам от тире
                    text = Regex.Replace(text, @"[^0-9\.\-]+", "");
                    // Ограничение на количество тире
                    int tireCount = text.Count(c => c == '-');
                    if (tireCount > 1)
                    {
                        // Удаление последнего тире
                        text = text.Substring(0, text.Length - 1);
                    }
                    // Ограничение на количество точек
                    int dotCount = text.Count(c => c == '.');
                    if (dotCount > 2 && tireCount != 1 || dotCount > 4 && tireCount == 1)
                    {
                        // Удаление последней точки
                        text = text.Substring(0, text.Length - 1);
                    }

                    // Ограничение на количество цифр подряд
                    int consecutiveDigitsCount = 0;
                    int dot = 0;
                    for (int i = 0; i < text.Length; i++)
                    {
                        // Если текущий символ является цифрой
                        if (char.IsDigit(text[i]))
                        {
                            consecutiveDigitsCount++;

                            //Если количество цифр подряд равно 4 вставляем тире
                            if (consecutiveDigitsCount > 4 && tireCount != 1)
                            {
                                text = text.Insert(i, "-");
                                i++;
                            }
                            else
                            {
                                if (consecutiveDigitsCount > 4)//если больше 4, но тире было
                                {
                                    // Удаление последней цифры
                                    text = text.Substring(0, text.Length - 1);
                                }
                            }

                        }
                        else
                        {
                            //плюсуем точки, пока не встретим тире
                            if (text[i] == '.')
                            {
                                dot++;
                                if (dot > 2 || i == 0)//если до или после тире стоит больше 2 точек или точка идет первая, обрубаем
                                {
                                    // Удаление последней точки
                                    text = text.Substring(0, text.Length - 1);
                                    dot--;
                                }
                                if (i != 0)
                                {
                                    if (text[i - 1] == '.' || text[i - 1] == '-')//если до этого точка или тире
                                    {
                                        // Удаление последней точки
                                        text = text.Substring(0, text.Length - 1);
                                        dot--;
                                    }

                                    if (i > 4)
                                        if (char.IsDigit(text[i - 1]) && char.IsDigit(text[i - 2]) && char.IsDigit(text[i - 3]) && char.IsDigit(text[i - 4]))//если четыре цифры
                                        {
                                            // Удаление последней точки
                                            text = text.Substring(0, text.Length - 1);
                                            dot--;
                                        }
                                }


                            }
                            else if (text[i] == '-')
                            {
                                dot = 0;
                                if (i == 0)//если первое, обрубаем
                                {
                                    // Удаление последней точки
                                    text = text.Substring(0, text.Length - 1);
                                    dot--;
                                }
                                if (i != 0)
                                {
                                    if (text[i - 1] == '.')//если до этого была точка обрубаем
                                    {
                                        // Удаление последней точки
                                        text = text.Substring(0, text.Length - 1);
                                        dot--;
                                    }
                                }
                            }
                            consecutiveDigitsCount = 0;
                            // Если количество цифр подряд равно 3, то не даем ничего ставить кроме цифры
                            if (i >= 4 && char.IsDigit(text[i - 1]) && char.IsDigit(text[i - 2]) && char.IsDigit(text[i - 3]) && !(char.IsDigit(text[i - 4]))
                            || i == 3 && char.IsDigit(text[i - 1]) && char.IsDigit(text[i - 2]) && char.IsDigit(text[i - 3]))
                            {
                                if (text[i] == '.' || text[i] == '-')
                                {
                                    text = text.Substring(0, text.Length - 1);
                                }
                            }
                        }
                    }

                    entry3.Text = text;
                }
                catch (Exception ex)
                {
                    // Обработка исключения
                    //обработки не будет, все на совести пользователя
                }
            };

            StackLayout stackLayout = new StackLayout();

            stackLayout.Children.Add(entry3);
            buttonsGrid.Children.Add(stackLayout, 0, 2); // Grid.Row="2"

        }
        private async void Button3_1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3_1(ServiceProvider, BookId,NamePerson,"Биография"));
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