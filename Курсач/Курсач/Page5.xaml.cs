using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Курсач
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page5 : ContentPage
    {
        private IServiceProvider ServiceProvider { get; set; }
        private int BookId;
        public Page5(IServiceProvider serviceProvider, int id)
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
            var position = await GetCurrentLocationAsync();

            string html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""utf-8"">
                    <title>Яндекс Карта</title>
                    <script src=""https://api-maps.yandex.ru/2.1/?apikey=8d1dfeaa-d879-41ef-a51a-29cdc17e0656&lang=ru_RU"" type=""text/javascript""></script>
                    <style>
                        html, body, #map {{
                            width: 100%;
                            height: 100%;
                            margin: 0;
                        }}

                    </style>
                </head>
                <body>
                    <div id=""map""></div>
                    <script type=""text/javascript"">
                        ymaps.ready(init);
                        function init() {{
                            var myMap = new ymaps.Map('map', {{
                                center: [58.0105, 56.2505], // Используем текущее местоположение
                                zoom: 10
                            }});
                        
                        // Добавляем маркер на текущее местоположение
                        var myPlacemark = new ymaps.Placemark([58.0105, 56.2505], {{
                            hintContent: 'Это ваше местоположение!',
                            balloonContent: 'Вы в Перми.'
                        }});

                        // Добавляем маркер на карту
                        myMap.geoObjects.add(myPlacemark);

                        // Обработчик события клика по карте
                        myMap.events.add('click', function (e) {{
                            var coords = e.get('coords');
                            var markerName = prompt('Введите название метки:');
                            if (markerName) {{
                                var newPlacemark = new ymaps.Placemark(coords, {{
                                    hintContent: markerName,
                                    balloonContent: 'Метка: ' + markerName
                                }});
                                myMap.geoObjects.add(newPlacemark);
                            }}
                        }});
                    }}
                    </script>
                </body>
                </html>
            ";

            mapWebView.Source = new HtmlWebViewSource { Html = html };
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