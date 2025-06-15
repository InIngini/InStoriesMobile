using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InStories.Core.DB;
using InStories.Core.DB.Interfaces;
using InStories.Core.Services;
using System.IO;
using InStories.Core.Services.Interfaces;
using InStories.Core.Common;
using System.Threading.Tasks;
using Xamarin.Essentials;
using InStories.Core.Data.CommonModels;

namespace InStories
{
    public partial class App : Application
    {
        private readonly IServiceProvider ServiceProvider;
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            try
            {
                InitializeComponent();
                LoadConfiguration();
                ValidateConfiguration();
                ServiceProvider = ConfigureServices();

                if (ServiceProvider == null)
                    throw new InvalidOperationException("Не удалось создать ServiceProvider");

                MainPage = new NavigationPage(new MainPage(ServiceProvider));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации App: {ex}");
                throw;
            }
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Регистрация конфигурации
            services.AddSingleton(Configuration);

            // Регистрация репозиториев
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IDatabaseSyncService, DatabaseSyncService>();

            // Регистрация сервисов
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<ITimelineService, TimelineService>();
            services.AddScoped<IUserService, UserService>();

            // Регистрация обработчиков
            services.AddTransient<AuthorizationHandler>();

            // Регистрация HttpClient для сервисов
            RegisterHttpClient<IUserService, UserService>(services, Configuration);
            RegisterHttpClient<IBookService, BookService>(services, Configuration, true);
            RegisterHttpClient<ICharacterService, CharacterService>(services, Configuration, true);
            RegisterHttpClient<IConnectionService, ConnectionService>(services, Configuration, true);
            RegisterHttpClient<IEventService, EventService>(services, Configuration, true);
            RegisterHttpClient<IGalleryService, GalleryService>(services, Configuration, true);
            RegisterHttpClient<IPictureService, PictureService>(services, Configuration, true);
            RegisterHttpClient<ISchemeService, SchemeService>(services, Configuration, true);
            RegisterHttpClient<ITimelineService, TimelineService>(services, Configuration, true);

            return services.BuildServiceProvider();
        }

        private void RegisterHttpClient<TService, TImplementation>(
            IServiceCollection services,
            IConfiguration configuration,
            bool addAuthorizationHandler = false)
            where TService : class
            where TImplementation : class, TService
        {
            var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();

            if (apiSettings == null)
                throw new InvalidOperationException("Настройки API не найдены в конфигурации");

            var clientBuilder = services.AddHttpClient<TService, TImplementation>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(apiSettings.BaseUrl);
                    client.Timeout = TimeSpan.FromSeconds(30);
                });

            clientBuilder.AddHttpMessageHandler(() => new PingHandler(apiSettings.Host, apiSettings.Port));

            if (addAuthorizationHandler)
            {
                clientBuilder.AddHttpMessageHandler<AuthorizationHandler>();
            }
        }

        private void LoadConfiguration()
        {
            try
            {
                var assembly = typeof(App).Assembly;
                using (var stream = assembly.GetManifestResourceStream("InStories.appsettings.json"))
                {
                    if (stream == null)
                        throw new FileNotFoundException("appsettings.json не найден. Убедитесь, что файл добавлен как Embedded Resource.");

                    Configuration = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки конфигурации: {ex}");
                throw;
            }
        }

        private void ValidateConfiguration()
        {
            var apiSettings = Configuration.GetSection("ApiSettings").Get<ApiSettings>();

            if (apiSettings == null ||
                string.IsNullOrEmpty(apiSettings.BaseUrl) ||
                string.IsNullOrEmpty(apiSettings.Host) ||
                string.IsNullOrEmpty(apiSettings.Port))
            {
                throw new InvalidOperationException("Неверная конфигурация API в appsettings.json");
            }
        }

        protected override async void OnStart()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                await CheckAndRequestPermissions();
            }
        }

        private async Task CheckAndRequestPermissions()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе разрешений: {ex}");
            }
        }

        protected override void OnSleep()
        {
            // Обработка перехода приложения в фоновый режим
        }

        protected override void OnResume()
        {
            // Обработка возврата приложения на передний план
        }
    }
}