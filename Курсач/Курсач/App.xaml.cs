using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Курсач.Core.DB;
using Курсач.Core.DB.Interfaces;
using Курсач.Core.Services;
using System.Configuration;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using Курсач.Core.Services.Interfaces;
using Курсач.Core.Common;
using Курсач.Core.Data;

namespace Курсач
{
    public partial class App : Application
    {
        private readonly IServiceProvider ServiceProvider;
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            InitializeComponent();

            LoadConfiguration();

            ServiceProvider = OnConfiguration();

            MainPage = new NavigationPage(new MainPage(ServiceProvider));
        }

        public IServiceProvider OnConfiguration()
        {
            var services = new ServiceCollection();

            services.AddSingleton(Configuration);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IDatabaseSyncService, DatabaseSyncService>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<ITimelineService, TimelineService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<AuthorizationHandler>();

            services.AddScoped<IMapRepository, MapRepository>();
            services.AddScoped<IWebMessageHandler, MarkerManager>();

            RegisterHttpClient<IUserService, UserService>(services);
            RegisterHttpClient<IBookService, BookService>(services, true);
            RegisterHttpClient<ICharacterService, CharacterService>(services, true);
            RegisterHttpClient<IConnectionService, ConnectionService>(services, true);
            RegisterHttpClient<IEventService, EventService>(services, true);
            RegisterHttpClient<IGalleryService, GalleryService>(services, true);
            RegisterHttpClient<IPictureService, PictureService>(services, true);
            RegisterHttpClient<ISchemeService, SchemeService>(services, true);
            RegisterHttpClient<ITimelineService, TimelineService>(services, true);

            return services.BuildServiceProvider();
        }

        private void RegisterHttpClient<TService, TImplementation>(IServiceCollection services, bool addAuthorizationHandler = false)
            where TService : class
            where TImplementation : class, TService
        {
            var clientBuilder = services.AddHttpClient<TService, TImplementation>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("http://192.168.1.33:5153/");
                });
            clientBuilder.AddHttpMessageHandler(() => new PingHandler("192.168.1.33","5153"));
            if (addAuthorizationHandler)
            {
                clientBuilder.AddHttpMessageHandler<AuthorizationHandler>();
            }
        }

        private void LoadConfiguration()
        {
            var assembly = typeof(App).Assembly;
            using (var stream = assembly.GetManifestResourceStream("Курсач.appsettings.json"))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("appsettings.json не найден.");
                }

                var builder = new ConfigurationBuilder()
                    .AddJsonStream(stream);

                Configuration = builder.Build();
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
