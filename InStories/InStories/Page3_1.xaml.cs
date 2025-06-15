using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InStories.Core.DB.Interfaces;
using InStories.Core.Services.Interfaces;
using InStories.Core.Data.Entities;
using System.Diagnostics;
using System.Runtime.InteropServices;
using InStories.Core.Data.DTO;
using InStories.Core.Errors;
using InStories.Core.Services;
using InStories.Core.Data.Guide;

namespace InStories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3_1 : ContentPage
    {
        private int BookId;
        private int CharacterId;
        private string NameCharacter;
        private string Кнопка;
        private IServiceProvider ServiceProvider { get; set; }
        private IBookService BookService { get; set; }
        private ICharacterService CharacterService { get; set; }
        public Page3_1(IServiceProvider serviceProvider, int bookId, int characterId, string кнопка)
        {
            ServiceProvider = serviceProvider;
            BookService = ServiceProviderServiceExtensions.GetService<IBookService>(ServiceProvider);
            CharacterService = ServiceProviderServiceExtensions.GetService<ICharacterService>(ServiceProvider);

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BookId = bookId;
            CharacterId = characterId;
            Кнопка = кнопка;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadQuestionAsync();
            await LoadDataAsync();

            if (Кнопка == "Биография")
                ScrollToIndex(5);
        }

        private async Task LoadQuestionAsync()
        {
            // Получаем вопросы с сервера
            var questions = await CharacterService.GetQuestions();

            if (questions is null || questions.Count() == 0)
                throw new ArgumentNullException("У вас нет доступа к интернету...");

            // Очищаем списки перед заполнением
            textList1.Clear();
            textList2.Clear();
            textList3.Clear();
            textList4.Clear();

            textList1.Add("Имя");

            // Группируем вопросы по блокам
            foreach (var question in questions)
            {
                switch (question.Block)
                {
                    case "Личность":
                        textList1.Add(question.QuestionText);
                        break;
                    case "Внешность":
                        textList2.Add(question.QuestionText);
                        break;
                    case "Характер":
                        textList3.Add(question.QuestionText);
                        break;
                    case "По истории":
                        textList4.Add(question.QuestionText);
                        break;
                    default:
                        // Можно добавить логирование для нераспознанных блоков
                        Debug.WriteLine($"Неизвестный блок вопроса: {question.Block}");
                        break;
                }
            }
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

            if (Кнопка == "Личность")
            {
                Button_Clicked(button1, EventArgs.Empty);//по умолчанию открыта личность
            }
            else
            {
                Button_Clicked(button5, EventArgs.Empty);//Биография
            }
        }

        private List<string> textList1 = new List<string>(); // Личность
        private List<string> textList2 = new List<string>(); // Внешность
        private List<string> textList3 = new List<string>(); // Характер
        private List<string> textList4 = new List<string>(); // По истории

        private Entry _nameEntry; // Сохраняем ссылку на поле ввода имени
        private Label _namePersonLabel; // Сохраняем ссылку на метку имени

        bool АватарОбновить = true;
        private async void ScrollToIndex(int index)
        {
            await characteristicScrollView.ScrollToAsync((index - 1) * 100, 0, true);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CharacterId == 0)
                await CreateCharacter();
            else
                await SaveCharacter();

            var character = await CharacterService.GetCharacter(CharacterId);

            NameCharacter = character.Name;

            if (АватарОбновить)
            {
                buttonsGrid.Children.Clear();

                Image avatarImage = new Image
                {
                    Source = "avatarBig.png",
                    WidthRequest = 137,
                    HeightRequest = 137,
                    Margin = new Thickness(0, 0, 0, 0),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                _namePersonLabel = new Label
                {
                    Text = NameCharacter,
                    TextColor = Color.White,
                    FontFamily = "Istok Web Bold",
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                buttonsGrid.Children.Add(avatarImage, 0, 0);
                buttonsGrid.Children.Add(_namePersonLabel, 1, 0);

                АватарОбновить = false;
            }

            Button clickedButton = (Button)sender;

            // Изменение цвета кнопок
            button1.TextColor = Color.Black;
            button2.TextColor = Color.Black;
            button3.TextColor = Color.Black;
            button4.TextColor = Color.Black;
            button5.TextColor = Color.Black;
            button6.TextColor = Color.Black;
            clickedButton.TextColor = Color.White;
            int indexButton = clickedButton.AutomationId[0] - '0';
            ScrollToIndex(indexButton);

            buttonsGrid2.Children.Clear();
            ScrollView scrollView = new ScrollView();

            string buttonText = clickedButton.Text; // Получаем текст нажатой кнопки
            
            switch (buttonText)
            {
                case "Личность":
                    var indexInAnswer = 0; // Для имени еще
                    StackLayout stackLayout = new StackLayout()
                    {
                        Margin = new Thickness(0, 14, 0, 0),
                    };
                    foreach (string text in textList1)
                    {
                        Label subtitleLabel1 = new Label//заголовок свойства
                        {
                            Margin = new Thickness(19, 0, 0, 0),
                            Text = text,
                            FontFamily = "Istok Web Bold",
                            FontSize = 20,
                            TextColor = Color.White,
                            FontAttributes = FontAttributes.Bold
                        };
                        stackLayout.Children.Add(subtitleLabel1);
   
                        // Entry
                        Entry entry1 = new Entry
                        {
                            HeightRequest = 50,
                            Margin = new Thickness(15, 0, 0, 0),
                            Text = indexInAnswer == 0 ? character.Name : character.Answers[indexInAnswer-1],
                            AutomationId = $"{indexInAnswer}",
                            FontFamily = "Istok Web",
                            FontSize = 20,
                            TextColor = Color.White,
                            BackgroundColor = Color.Transparent
                        };

                        // Если это поле с именем (первый элемент)
                        if (indexInAnswer == 0)
                        {
                            _nameEntry = entry1;
                            _nameEntry.TextChanged += OnNameEntryTextChanged;
                        }

                        indexInAnswer++;
                        stackLayout.Children.Add(entry1);
                    }
                    scrollView.Content = stackLayout;

                    break;

                case "Внешность":
                    var indexInAnswer2 = textList1.Count;
                    StackLayout stackLayout2 = new StackLayout()
                    {
                        Margin = new Thickness(0, 14, 0, 0),
                    };
                    //добавляем две кнопочки
                    // Создание первой кнопки "Создать аватар"
                    Button создатьАватар = new Button
                    {
                        Text = "Создать аватар",
                        BackgroundColor = Color.FromHex("#B280FE"),
                        TextColor = Color.White,
                        CornerRadius = 12,
                        WidthRequest = 162.85,
                        HeightRequest = 66.81,
                        FontFamily = "Istok Web Bold",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        Margin = new Thickness(10, 0, 10, 10),
                    };
                    создатьАватар.Clicked += создатьАватар_Clicked;
                    // Создание второй кнопки "Загрузить фото"
                    Button загрузитьФото = new Button
                    {
                        Text = "Загрузить фото",
                        BackgroundColor = Color.FromHex("#B280FE"),
                        TextColor = Color.White,
                        CornerRadius = 12,
                        WidthRequest = 162.85,
                        HeightRequest = 66.81,
                        FontFamily = "Istok Web Bold",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        Margin = new Thickness(10, 0, 10, 10),
                    };
                    загрузитьФото.Clicked += загрузитьФото_Clicked;
                    // Создание Grid
                    Grid grid = new Grid();
                    // Добавление кнопок в Grid
                    grid.Children.Add(создатьАватар, 0, 0); // первая колонка, первая строка
                    grid.Children.Add(загрузитьФото, 1, 0); // вторая колонка, первая строка

                    stackLayout2.Children.Add(grid);

                    foreach (string text in textList2)
                    {
                        Label subtitleLabel2 = new Label//заголовок свойства
                        {
                            Margin = new Thickness(19, 0, 0, 0),
                            Text = text,
                            FontFamily = "Istok Web Bold",
                            FontSize = 20,
                            TextColor = Color.White,
                            FontAttributes = FontAttributes.Bold
                        };
                        stackLayout2.Children.Add(subtitleLabel2);

                        Entry entry2 = new Entry
                        {
                            HeightRequest = 50,
                            Margin = new Thickness(15, 0, 0, 0),
                            Text = character.Answers[indexInAnswer2-1],
                            AutomationId = $"{indexInAnswer2}",
                            FontFamily = "Istok Web",
                            FontSize = 20,
                            TextColor = Color.White,
                            BackgroundColor = Color.Transparent
                        };
                        indexInAnswer2++;
                        stackLayout2.Children.Add(entry2);
                    }

                    scrollView.Content = stackLayout2;
                    break;

                case "Характер":
                    var indexInAnswer3 = textList1.Count+textList2.Count;
                    StackLayout stackLayout3 = new StackLayout()
                    {
                        Margin = new Thickness(0, 14, 0, 0),
                    };
                    foreach (string text in textList3)
                    {
                        Label subtitleLabel3 = new Label//заголовок свойства
                        {
                            Margin = new Thickness(19, 0, 0, 0),
                            Text = text,
                            FontFamily = "Istok Web Bold",
                            FontSize = 20,
                            TextColor = Color.White,
                            FontAttributes = FontAttributes.Bold
                        };
                        stackLayout3.Children.Add(subtitleLabel3);

                        // Entry
                        Entry entry3 = new Entry
                        {
                            HeightRequest = 50,
                            Margin = new Thickness(15, 0, 0, 0),
                            Text = character.Answers[indexInAnswer3-1],
                            AutomationId = $"{indexInAnswer3}",
                            FontFamily = "Istok Web",
                            FontSize = 20,
                            TextColor = Color.White,
                            BackgroundColor = Color.Transparent
                        };
                        stackLayout3.Children.Add(entry3);
                    }
                    scrollView.Content = stackLayout3;
                    break;

                case "По истории":
                    var indexInAnswer4 = textList1.Count + textList2.Count + textList3.Count;
                    StackLayout stackLayout4 = new StackLayout()
                    {
                        Margin = new Thickness(0, 14, 0, 0),

                    };
                    foreach (string text in textList4)
                    {
                        Label subtitleLabel4 = new Label//заголовок свойства
                        {
                            Margin = new Thickness(19, 0, 0, 0),
                            Text = text,
                            FontFamily = "Istok Web Bold",
                            FontSize = 20,
                            TextColor = Color.White,
                            FontAttributes = FontAttributes.Bold
                        };
                        stackLayout4.Children.Add(subtitleLabel4);

                        // Editor
                        Editor editor4 = new Editor
                        {
                            Margin = new Thickness(15, 0, 0, 0),
                            Text = character.Answers[indexInAnswer4-1],
                            AutomationId = $"{indexInAnswer4}",
                            FontFamily = "Istok Web",
                            FontSize = 20,
                            TextColor = Color.White,
                            BackgroundColor = Color.Transparent,
                            WidthRequest = 360,
                            HeightRequest = 100
                        };
                        indexInAnswer4++;
                        stackLayout4.Children.Add(editor4);

                    }
                    scrollView.Content = stackLayout4;
                    break;

                case "Биография":
                    Grid gridBio = new Grid();
                    int i = 0;
                    List<string> buttonTexts = new List<string> { "Добавить новую запись", "Заголовок", "дд.мм.гггг", "Тут будет написана какая-то биография" };
                    foreach (string text in buttonTexts)
                    {
                        Label label = new Label
                        {
                            Text = text,
                            TextColor = Color.White,
                            FontSize = 20,
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontFamily = "Istok Web Bold",
                            FontAttributes = FontAttributes.Bold,
                            Margin = new Thickness(7, 15, 0, 0),
                        };
                        if (text.Length > 30)
                        {
                            label.Text = label.Text.Substring(0, 30) + "...";
                        }

                        BoxView stackedButton = new BoxView
                        {
                            BackgroundColor = Color.FromHex("#B280FE"),
                            CornerRadius = 12,
                            HeightRequest = 45,

                        };
                        StackLayout buttonContent = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 5,
                            Children = { label },

                        };
                        if (text == "Добавить новую запись")
                        {
                            Label plusik = new Label
                            {
                                Text = "+",
                                TextColor = Color.White,
                                FontSize = 33,
                                HorizontalTextAlignment = TextAlignment.Center,
                                FontFamily = "Istok Web Bold",
                                FontAttributes = FontAttributes.Bold,
                                Margin = new Thickness(0, 7, 15, 0),
                                HorizontalOptions = LayoutOptions.EndAndExpand
                            };
                            buttonContent.Children.Add(plusik);
                        }
                        Button stackedButton2 = new Button
                        {
                            Padding = new Thickness(0),
                            BackgroundColor = Color.Transparent,
                            CornerRadius = 12,
                            HeightRequest = 45,
                            AutomationId = text,

                        };
                        stackedButton2.Clicked += Button3_2_Clicked;

                        gridBio.RowSpacing = 10;
                        gridBio.Children.Add(stackedButton, 0, i);
                        gridBio.Children.Add(buttonContent, 0, i);
                        gridBio.Children.Add(stackedButton2, 0, i);
                        i++;
                    }
                    scrollView.Content = gridBio;
                    break;

                case "Галерея":
                    string[] фотки = new string[]
                    {
                        "фото1",
                        "фото2",
                        "фото3",
                        "фотоо4",
                        "фото1",
                        "фото2",
                        "фото3",
                        "фотоо4",
                    };

                    Grid gridGalery = new Grid();

                    for (int j = 0; j < фотки.Length; j++)
                    {
                        Button button = new Button
                        {
                            AutomationId = фотки[j],
                            BackgroundColor = Color.White,
                            CornerRadius = 12,
                            WidthRequest = 158,
                            HeightRequest = 188,
                            Margin = new Thickness(10, 10, 10, 10)
                        };

                        gridGalery.Children.Add(button, j % 2, j / 2);
                    }
                    scrollView.Content = gridGalery;
                    break;
                default:
                    // Действие по умолчанию, если нажата кнопка с неизвестным текстом (но вообще таких нет)
                    break;
            }

            buttonsGrid2.Children.Add(scrollView);

        }
        Button button2_1, button2_2, button2_3, button2_4;
        Grid внешность;
        private void создатьАватар_Clicked(object sender, EventArgs e)
        {
            buttonsGrid2.Children.Clear();
            buttonsGrid.Children.Clear();

            Image avatarImage = new Image
            {
                Source = "avatarBig.png",
                WidthRequest = 137,
                HeightRequest = 137,
                Margin = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            АватарОбновить = true;

            buttonsGrid.Children.Add(avatarImage, 0, 0);
            Grid.SetColumnSpan(avatarImage, 2); // Устанавливаем объединение колонок на 2

            внешность = new Grid();
            внешность.RowDefinitions.Add(new RowDefinition { Height = new GridLength(46) });
            внешность.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            // Создание ScrollView с горизонтальной ориентацией
            ScrollView scrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal
            };

            // Создание StackLayout с горизонтальной ориентацией
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("#B280FE"),
            };

            // Создание кнопок
            button2_1 = new Button
            {
                Text = "Лицо",
                BackgroundColor = Color.FromHex("#B280FE"),
                TextColor = Color.White,
                FontSize = 20,
                FontFamily = "Istok Web Bold",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            button2_2 = new Button
            {
                Text = "Глаза",
                BackgroundColor = Color.FromHex("#B280FE"),
                TextColor = Color.Black,
                FontSize = 20,
                FontFamily = "Istok Web Bold",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            button2_3 = new Button
            {
                Text = "Волосы",
                BackgroundColor = Color.FromHex("#B280FE"),
                TextColor = Color.Black,
                FontSize = 20,
                FontFamily = "Istok Web Bold",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            button2_4 = new Button
            {
                Text = "Особенности",
                BackgroundColor = Color.FromHex("#B280FE"),
                TextColor = Color.Black,
                FontSize = 20,
                FontFamily = "Istok Web Bold",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            button2_1.Clicked += ButtonНижние_Clicked;
            button2_2.Clicked += ButtonНижние_Clicked;
            button2_3.Clicked += ButtonНижние_Clicked;
            button2_4.Clicked += ButtonНижние_Clicked;

            // Добавление кнопок в StackLayout
            stackLayout.Children.Add(button2_1);
            stackLayout.Children.Add(button2_2);
            stackLayout.Children.Add(button2_3);
            stackLayout.Children.Add(button2_4);

            // Добавление StackLayout в ScrollView
            scrollView.Content = stackLayout;

            // Размещение ScrollView в Grid
            внешность.Children.Add(scrollView, 0, 0);

            // Размещение ScrollView на странице
            buttonsGrid2.Children.Add(внешность);

            ButtonНижние_Clicked(button2_1, EventArgs.Empty);//по умолчанию открыто лицо
        }
        private void ButtonНижние_Clicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Проверяем наличие элементов во второй строке
            if (внешность.Children.Count > 0)
            {
                // Удаляем элементы только из второй строки
                for (int i = внешность.Children.Count - 1; i >= 0; i--)
                {
                    var child = внешность.Children[i];
                    if (Grid.GetRow(child) == 1)
                    {
                        внешность.Children.RemoveAt(i);
                    }
                }
            }

            // Изменение цвета кнопок
            button2_1.TextColor = Color.Black;
            button2_2.TextColor = Color.Black;
            button2_3.TextColor = Color.Black;
            button2_4.TextColor = Color.Black;
            clickedButton.TextColor = Color.White;

            string buttonText = clickedButton.Text; // Получаем текст нажатой кнопки

            Grid grid = new Grid();
            int buttonCounter = 0;

            // Создание кнопок для соответствующего массива строк
            switch (buttonText)
            {
                case "Лицо":
                    string[] лицо = new string[]
                    {
                        "лицо1",
                        "лицо2",
                        "лицо3",
                        "лицо4",
                    };

                    for (int i = 0; i < лицо.Length; i++)
                    {
                        Button button = new Button
                        {
                            AutomationId = лицо[i],
                            BackgroundColor = Color.White,
                            CornerRadius = 12,
                            WidthRequest = 158,
                            HeightRequest = 188,
                            Margin = new Thickness(10, 10, 10, 10)
                        };

                        grid.Children.Add(button, buttonCounter % 2, buttonCounter / 2);
                        buttonCounter++;
                    }

                    break;

                case "Глаза":
                    string[] глаза = new string[]
                    {
                        "глаза1",
                        "глаза2",
                        "глаза3"
                    };

                    for (int i = 0; i < глаза.Length; i++)
                    {
                        Button button = new Button
                        {
                            AutomationId = глаза[i],
                            BackgroundColor = Color.White,
                            CornerRadius = 12,
                            WidthRequest = 158,
                            HeightRequest = 188,
                            Margin = new Thickness(10, 10, 10, 10)
                        };

                        grid.Children.Add(button, buttonCounter % 2, buttonCounter / 2);
                        buttonCounter++;
                    }

                    break;

                case "Волосы":
                    string[] волосы = new string[]
                    {
                        "волосы1",
                        "волосы2",
                        "волосы3",
                        "волосы4",
                        "волосы5",
                        "волосы6"
                    };

                    for (int i = 0; i < волосы.Length; i++)
                    {
                        Button button = new Button
                        {
                            AutomationId = волосы[i],
                            BackgroundColor = Color.White,
                            CornerRadius = 12,
                            WidthRequest = 158,
                            HeightRequest = 188,
                            Margin = new Thickness(10, 10, 10, 10)
                        };

                        grid.Children.Add(button, buttonCounter % 2, buttonCounter / 2);
                        buttonCounter++;
                    }

                    break;

                case "Особенности":
                    string[] особенности = new string[]
                    {
                        "особенность1",
                        "особенность2",
                        "особенность3",
                        "особенность4",
                        "особенность5"
                    };

                    for (int i = 0; i < особенности.Length; i++)
                    {
                        Button button = new Button
                        {
                            AutomationId = особенности[i],
                            BackgroundColor = Color.White,
                            CornerRadius = 12,
                            WidthRequest = 158,
                            HeightRequest = 188,
                            Margin = new Thickness(10, 10, 10, 10)
                        };

                        grid.Children.Add(button, buttonCounter % 2, buttonCounter / 2);
                        buttonCounter++;
                    }

                    break;

                default:
                    // Действие по умолчанию, если нажата кнопка с неизвестным текстом
                    break;
            }

            // Установка скроллируемой области внутри ScrollView
            ScrollView scrollView = new ScrollView
            {
                Content = grid
            };

            // Добавление ScrollView в нужный Grid
            внешность.Children.Add(scrollView, 0, 1);
        }
        private void загрузитьФото_Clicked(object sender, EventArgs e)
        {

        }
        private async void Button3_2_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.AutomationId;

            await Navigation.PushAsync(new Page3_2(ServiceProvider, BookId, text, CharacterId));

        }

        private async Task CreateCharacter()
        {
            try
            {
                if (CharacterId == 0)
                {
                    // Создание нового персонажа
                    var newCharacter = new Character
                    {
                        BookId = BookId,
                        Name = NameCharacter ?? "Новый персонаж",
                        PictureId = GetCurrentPictureId() // Ваш метод для получения PictureId
                    };

                    var createdCharacter = await CharacterService.CreateCharacter(newCharacter);
                    CharacterId = createdCharacter.Id;
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Ошибка", $"Не удалось создать персонажа: {ex.Message}", "OK");
                throw new Exception($"Ошибка сохранения: {ex}");
            }
        }
        private async Task SaveCharacter()
        {
            try
            {
                // 1. Собираем данные из интерфейса
                var answers = CollectAnswersFromUI();

                // 2. Подготавливаем данные для сохранения
                if (CharacterId == 0)
                {
                    await CreateCharacter();

                    // Сразу обновляем ответы для нового персонажа
                    await UpdateCharacterAnswers(answers);
                }
                else
                {
                    // Обновление существующего персонажа
                    await UpdateCharacterAnswers(answers);
                }
            }
            catch (Exception ex)
            {
                
                await DisplayAlert("Ошибка", $"Не удалось сохранить персонажа: {ex.Message}", "OK");
                throw new Exception($"Ошибка сохранения: {ex}");
            }
        }

        private List<string> CollectAnswersFromUI()
        {
            var answers = new List<string>();

            if (buttonsGrid2.Children.FirstOrDefault() is ScrollView scrollView)
            {
                switch (scrollView.Content)
                {
                    case StackLayout stackLayout:
                        foreach (var view in stackLayout.Children)
                        {
                            switch (view)
                            {
                                case Entry entry:
                                    if (Convert.ToInt32(entry.AutomationId) != 0) // Пропускаем имя, нулевое
                                        answers.Add(entry.Text ?? string.Empty);
                                    break;
                                case Editor editor:
                                    if (Convert.ToInt32(editor.AutomationId) != 0)
                                        answers.Add(editor.Text ?? string.Empty);
                                    break;
                            }
                        }
                        break;

                    case Grid grid:
                        // Обработка Grid (если нужно)
                        break;
                }
            }

            return answers;
        }

        private async Task UpdateCharacterAnswers(List<string> answers)
        {
            var updateData = new CharacterWithAnswers
            {
                Name = NameCharacter ?? "Новый персонаж",
                Answers = answers.ToArray(),
                PictureId = GetCurrentPictureId() // Ваш метод для получения PictureId
            };

            await CharacterService.UpdateCharacter(updateData, CharacterId);
        }

        // Новый обработчик изменений в поле имени
        private void OnNameEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_namePersonLabel != null && sender is Entry entry)
            {
                NameCharacter = entry.Text;
                _namePersonLabel.Text = entry.Text;

                // Если текст слишком длинный, обрезаем его
                if (entry.Text.Length > 13)
                {
                    _namePersonLabel.Text = entry.Text.Substring(0, 13) + "...";
                }
            }
        }

        private int? GetCurrentPictureId()
        {
            // Реализуйте логику получения текущего PictureId
            return null; // Заглушка
        }
        private async void ButtonHome_Clicked(object sender, EventArgs e)
        {
            await SaveCharacter();
            await Navigation.PushAsync(new Page2(ServiceProvider, BookId));
        }
        private async void ButtonPersona_Clicked(object sender, EventArgs e)
        {
            await SaveCharacter();
            await Navigation.PushAsync(new Page3(ServiceProvider, BookId));
        }
        private async void ButtonShema_Clicked(object sender, EventArgs e)
        {
            await SaveCharacter();
            await Navigation.PushAsync(new Page4(ServiceProvider, BookId));
        }
        private async void ButtonTime_Clicked(object sender, EventArgs e)
        {
            await SaveCharacter();
            await Navigation.PushAsync(new Page5(ServiceProvider, BookId));
        }
    }
}