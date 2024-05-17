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
    public partial class Page4 : ContentPage
    {
        private AbsoluteLayout layout;

        string nameBook;
        public Page4(string nameBook)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.nameBook = nameBook;

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
            Grid grid = new Grid();

            Image imageEllipse = new Image();
            imageEllipse.Source = "ellipse.png";
            imageEllipse.WidthRequest = 80;
            imageEllipse.HeightRequest = 80;
            grid.Children.Add(imageEllipse, 0, 0);

            Image image = new Image();
            image.Source = source;
            image.WidthRequest = 80;
            image.HeightRequest = 80;
            grid.Children.Add(image, 0, 0);

            Label label = new Label();
            label.Text = name;
            label.TextColor = Color.White;
            label.FontFamily = "Istok Web";
            label.FontSize = 14;
            label.VerticalOptions = LayoutOptions.Center;
            label.HorizontalOptions = LayoutOptions.Center;

            grid.Children.Add(label, 0, 1);

            return grid;
        }

        private async void Button4_1_Clicked(object sender, EventArgs e)
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