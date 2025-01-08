using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Курсач.Core.Interfaces;
using Курсач.Services;

namespace Курсач
{
    public partial class App : Application
    {
        private readonly IServiceProvider ServiceProvider;

        public App()
        {
            InitializeComponent();

            ServiceProvider = OnConfiguration();
            var userService = ServiceProviderServiceExtensions.GetService<IUserService>(ServiceProvider);

            MainPage = new NavigationPage(new MainPage(userService));
        }

        public IServiceProvider OnConfiguration()
        {
            var services = new ServiceCollection();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<ITimelineService, TimelineService>();
            services.AddScoped<IUserService, UserService>();

            RegisterHttpClient<IUserService, UserService> (services);
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
                    client.BaseAddress = new Uri("http://10.0.2.2:5153/");
                });

            if (addAuthorizationHandler)
            {
                clientBuilder.AddHttpMessageHandler<AuthorizationHandler>();
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
