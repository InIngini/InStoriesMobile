using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
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

            layout = new AbsoluteLayout();
            scrollView.Content = layout;

            //Shema();
        }
        //public void Shema()//тут короче создаем схему, когда будут таблички, надо будет форейч 
        //{
        //    // Добавление узлов
        //    AddNode("John Doe", "avatar.png", new Point(100, 100));
        //    AddNode("Jane Smith", "avatar.png", new Point(200, 200));

        //    // Добавление связи между узлами
        //    AddConnection("John Doe", "Jane Smith");
        //}
        //private void AddNode(string name, string avatarSource, Point position)
        //{
        //    // Создание элементов для узла (Image, Label и т.д.)
        //    var image = new Image { Source = avatarSource };
        //    var nameLabel = new Label { Text = name };

        //    // Добавление элементов в AbsoluteLayout
        //    layout.Children.Add(image, new Xamarin.Forms.Rectangle(position, new Size(50, 50)));
        //    layout.Children.Add(nameLabel, new Xamarin.Forms.Rectangle(position.X, position.Y + 50, 100, 20));

        //}

        //private void AddConnection(string parentName, string childName)
        //{
        //    // Получение координат узлов
        //    var parentPosition = GetNodePosition(parentName);
        //    var childPosition = GetNodePosition(childName);

        //    // Создание линии для связи
        //    var line = new Line
        //    {
        //        X1 = parentPosition.X + 25,
        //        Y1 = parentPosition.Y + 50,
        //        X2 = childPosition.X + 25,
        //        Y2 = childPosition.Y,
        //        StrokeThickness = 2
        //    };

        //    // Добавление линии в AbsoluteLayout
        //    layout.Children.Add(line);
        //}

        

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