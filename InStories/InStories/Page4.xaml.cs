using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InStories.Core.DB.Interfaces;

namespace InStories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page4 : ContentPage
    {
        private AbsoluteLayout layout;

        private IServiceProvider ServiceProvider { get; set; }
        private int BookId;
        public Page4(IServiceProvider serviceProvider, int id)
        {
            ServiceProvider = serviceProvider;

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

            // Создаем диаграмму
            layout = new AbsoluteLayout();
            layout.HorizontalOptions = LayoutOptions.Center;
            layout.VerticalOptions = LayoutOptions.Center;
            
            Shema(1);

            scrollView.Content = layout;
        }
       
        int[,] принадлежностькСхеме = new int[,]   //id_схемы, id_связи
        {
          {1,1}, {1,2}, {1,3}, {1,4}, {1,5 },
          {2,3}, {2,4}
        };
        int[,] связи = new int[,] //id_связи, тип связи (1-романтика прямая горизонтальная линия, 2-сиблингство, 3-ребенок линия вниз), id_персонаж1, id_персонажа2
        {
          {1,1,1,2 },
          {2,2,2,3 },
          {3,3,1,4 },
          {4,3,2,4 },
          {5,1,4,5 }
        };
        Dictionary<int, string> персонажи = new Dictionary<int, string>()
        { // id_персонажа, имя персонажа
            { 1, "Персонаж 1" },
            { 2, "Персонаж 2" },
            { 3, "Персонаж 3" },
            { 4, "Персонаж 4" },
            { 5, "Персонаж 5" }
        };
        List<int[]> связьИнфо;
        Dictionary<int, Point> расположение;
        public void Shema(int id_схемы)
        {
            связьИнфо = new List<int[]>();
            расположение = new Dictionary<int, Point>();
            for (int i = 0; i < принадлежностькСхеме.GetLength(0); i++)//получаем информацию о связях для этой схемы
            {
                if (принадлежностькСхеме[i, 0] == id_схемы)
                { 
                    for (int j = 0; j < связи.GetLength(0); j++)
                    {

                        if (связи[j, 0] == принадлежностькСхеме[i, 1])
                        {
                            List<int> инфоОСвязи = new List<int>();
                            инфоОСвязи.Add(связи[j, 1]);
                            инфоОСвязи.Add(связи[j, 2]);
                            инфоОСвязи.Add(связи[j, 3]);
                            связьИнфо.Add(инфоОСвязи.ToArray());
                        }

                    }
                }
            }

            foreach (var связь in связьИнфо)//перебираем связи (тип связи, айди персонажа1, айди персонажа2)
            {

                if (!расположение.ContainsKey(связь[1]))
                {
                    Point point = GetPoint(1,new Point(0, 0), связь[0]);
                    расположение[связь[1]] = point;
                }
                if (!расположение.ContainsKey(связь[2]))
                {
                    Point point = GetPoint(2,расположение[связь[1]], связь[0]);
                    расположение[связь[2]] = point;
                }
                
            }
            foreach(var idpoint in расположение)
            {
                int id = idpoint.Key;
                Point point = idpoint.Value;
                View nodeView = AddNode(id, point, персонажи[id], "avatar.png");
                
                layout.Children.Add(nodeView, new Rectangle(point.X, point.Y, 100, 100));
            }
            
        }
        public Point GetPoint(int number, Point sosedPoint, int type)
        {
            if(sosedPoint == Point.Zero)
            {
                if (number != 2 && type != 3)//что это не второй узел и это не ребенок
                {
                    //тут надо проверить, что в дикшенари нет уже такого поинта
                    int i = 0; int j = 0;
                    while (расположение.Any(kvp => kvp.Value.X == i && kvp.Value.Y == j))
                    {
                        i += 160;
                    }
                    return new Point(i, j);
                }
            }

            if(type==1)
            {
                int i = (int)sosedPoint.X;
                int j = (int)sosedPoint.Y;

                foreach (var kvp in расположение)//если это супруг, то все, что правее  и ниже или равно сдвигаем вправо еще на 160 
                {
                    if (kvp.Value.X > i && kvp.Value.Y>=j)
                    {
                        Point newPoint = new Point(kvp.Value.X + 160, kvp.Value.Y);
                        расположение[kvp.Key] = newPoint;
                    }
                }
                return new Point(i+160,j);
            }

            if (type == 2)
            {
                int i = (int)sosedPoint.X;
                int j = (int)sosedPoint.Y;

                //если это сиблинг, то ставим его в ближайшее свободное право/лево

                // Ищем ближайшее пустое место влево
                int leftX = i;
                while (расположение.ContainsValue(new Point(leftX - 160, j)))
                {
                    leftX -= 160;
                }

                // Ищем ближайшее пустое место вправо
                int rightX = i;
                while (расположение.ContainsValue(new Point(rightX + 160, j)))
                {
                    rightX += 160;
                }

                // Выбираем ближайшее из найденных мест
                int newX = Math.Abs(leftX - i) < Math.Abs(rightX - i) ? leftX : rightX;

                return new Point(newX+160, j);
            }

            if(type == 3)
            {
                //это для общего ребенка
                int i = (int)sosedPoint.X+80;
                int j = (int)sosedPoint.Y+160;

                //если это ребенок, то спускаемся вниз, а потом смотрим есть ли там место

                // Ищем ближайшее пустое место влево
                int leftX = i;
                while (расположение.ContainsValue(new Point(leftX - 160, j)))
                {
                    leftX -= 160;
                }

                // Ищем ближайшее пустое место вправо
                int rightX = i;
                while (расположение.ContainsValue(new Point(rightX + 160, j)))
                {
                    rightX += 160;
                }

                // Выбираем ближайшее из найденных мест
                int newX = Math.Abs(leftX - i) < Math.Abs(rightX - i) ? leftX : rightX;

                return new Point(newX, j);
            }

            return new Point(0,0);//если ничего не сработало
        }
        public View AddNode(int id, Point point, string name, string source)
        {
            // Создаем основной контейнер
            Grid grid = new Grid
            {
                RowDefinitions =
        {
            new RowDefinition { Height = GridLength.Auto }, // Аватар
            new RowDefinition { Height = GridLength.Auto }, // Имя
            new RowDefinition { Height = GridLength.Auto }, // Фамилия
            new RowDefinition { Height = GridLength.Auto }  // Дата
        },
                WidthRequest = 100,
                VerticalOptions = LayoutOptions.Start
            };

            // Аватар (центрирован)
            Image image = new Image
            {
                Source = source,
                WidthRequest = 80,
                HeightRequest = 80,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center
            };
            Grid.SetRow(image, 0);
            grid.Children.Add(image);

            // Разделяем имя и фамилию
            var nameParts = name.Split(new[] { ' ' }, 2);
            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string lastName = nameParts.Length > 1 ? nameParts[1] : "";

            // Имя (первая строка)
            Label firstNameLabel = new Label
            {
                Text = firstName,
                TextColor = Color.White,
                FontFamily = "Istok Web",
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            Grid.SetRow(firstNameLabel, 1);
            grid.Children.Add(firstNameLabel);

            // Фамилия (вторая строка, если есть)
            if (!string.IsNullOrEmpty(lastName))
            {
                Label lastNameLabel = new Label
                {
                    Text = lastName,
                    TextColor = Color.White,
                    FontFamily = "Istok Web",
                    FontSize = 14,
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                Grid.SetRow(lastNameLabel, 2);
                grid.Children.Add(lastNameLabel);
            }

            // Дата рождения (третья строка)
            Label dateLabel = new Label
            {
                Text = "ДД.ММ.ГГГГ", // Замените на реальные данные
                TextColor = Color.White,
                FontFamily = "Istok Web",
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            Grid.SetRow(dateLabel, 3);
            grid.Children.Add(dateLabel);

            return grid;
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