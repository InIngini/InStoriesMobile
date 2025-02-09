using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Курсач.Core.Data;
using Курсач.Core.Services.Interfaces;

namespace Курсач
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page5 : ContentPage
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IWebMessageHandler MarkerManager { get; set; }
        private int BookId;
        public Page5(IServiceProvider serviceProvider, int id)
        {
            ServiceProvider = serviceProvider;
            MarkerManager = ServiceProviderServiceExtensions.GetService<IWebMessageHandler>(ServiceProvider);
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            BookId = id;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
            mapWebView.Navigating += OnNavigating;
        }
        private void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.StartsWith("myapp://"))
            {
                e.Cancel = true; // Отменяем навигацию по URL
                var segments = e.Url.Split(new[] { "myapp://" }, StringSplitOptions.None)
                    .LastOrDefault() // Получаем последнюю часть после "myapp://"
                    ?.Split('/'); // Теперь разбиваем по '/'

                if (segments.Length == 4 && segments[0] == "addmarker")
                {
                    var name = Uri.UnescapeDataString(segments[1]);
                    var latitude = Convert.ToDouble(segments[2], CultureInfo.InvariantCulture);
                    var longitude = Convert.ToDouble(segments[3], CultureInfo.InvariantCulture);

                    // Вызывайте метод вашей бизнес-логики
                    MarkerManager.AddMarker(name, latitude, longitude);
                }
            }
        }
        private async Task LoadDataAsync()
        {
            var position = await GetCurrentLocationAsync();
            var markers = await MarkerManager.GetAllMarker();
            string html = GenerateHtmlWithMarkers(markers);
            mapWebView.Source = new HtmlWebViewSource { Html = html };
        }
        private string GenerateHtmlWithMarkers(List<Marker> markers)
        {
            var markersJson = JsonConvert.SerializeObject(markers.Select(marker => new
            {
                name = marker.Name,
                coords = new[] { marker.Latitude, marker.Longitude }
            }).ToList());

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Яндекс Карта</title>
    <script src='https://api-maps.yandex.ru/2.1/?apikey=8d1dfeaa-d879-41ef-a51a-29cdc17e0656&lang=ru_RU' type='text/javascript'></script>
    <style>
        html, body, #map {{
            width: 100%;
            height: 100%;
            margin: 0;
        }}
    </style>
</head>
<body>
    <div id='map'></div>
    <button onclick='deleteAllMarkers()'>Удалить все маркеры</button>
    <script type='text/javascript'>
        ymaps.ready(init);
        var myMap;
        function init() {{
            myMap = new ymaps.Map('map', {{ center: [58.0105, 56.2505], zoom: 10 }});
            var markers = {markersJson};

            for (let i = 0; i < markers.length; i++) {{
                var newPlacemark = new ymaps.Placemark(markers[i].coords, {{
                    hintContent: markers[i].name,
                    balloonContent: 'Метка: ' + markers[i].name
                }});
                myMap.geoObjects.add(newPlacemark);
            }}

            myMap.events.add('click', function (e) {{
                var coords = e.get('coords');
                var markerName = prompt('Введите название метки:');
                if (markerName) {{
                    var newPlacemark = new ymaps.Placemark(coords, {{
                        hintContent: markerName,
                        balloonContent: 'Метка: ' + markerName
                    }});
                    myMap.geoObjects.add(newPlacemark);

                    // Вызов C# метода через URL
                    window.location.href = 'myapp://addmarker/' + markerName + '/' + coords[0] + '/' + coords[1];
                }}
            }});

            window.deleteAllMarkers = function() {{
                // Вызов C# метода для удаления всех меток
                window.location.href = 'myapp://deleteAllMarkers'; 
            }};
        }}
    </script>
</body>
</html>";
        }

        private async void OnDeleteAllMarkersClicked(object sender, EventArgs e)
        {
            await MarkerManager.DeleteAllMarker(); // Вызываем метод для удаления всех меток
            await LoadDataAsync(); // Обновляем карту
        }
    
        public async Task<Position> GetCurrentLocationAsync()
        {
            try
            {
                // Запрашиваем текущее местоположение
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    return new Position(location.Latitude, location.Longitude);
                }
                else
                {
                    // Если последнее известное местоположение не найдено, запрашиваем новое
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));

                    if (location != null)
                    {
                        return new Position(location.Latitude, location.Longitude);
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Ошибка", "Устройство не поддерживает геолокацию", "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Ошибка", "Разрешение на доступ к местоположению не предоставлено", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Геолокация не доступна", "OK");
            }

            return new Position(37.79752, -122.40183); 
        }

        private async void Button5_1_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;  // Получение объекта Button, который отправил событие
            string buttonText = button.AutomationId;  // Получение текста кнопки

            await Navigation.PushAsync(new Page3_1(ServiceProvider, BookId, buttonText, "Личность"));
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