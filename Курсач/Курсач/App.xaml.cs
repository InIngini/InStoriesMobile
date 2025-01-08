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
		public App()
		{
			InitializeComponent();

            OnConfiguration();

            MainPage = new NavigationPage(new MainPage());
		}

		public void OnConfiguration()
		{
            var services = new ServiceCollection();

            services.AddHttpClient<UserService>(client =>{client.BaseAddress = new Uri("http://localhost:5153/");});
            services.AddHttpClient<TimelineService>(client => {client.BaseAddress = new Uri("http://localhost:5153/");});
            services.AddHttpClient<SchemeService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });
            services.AddHttpClient<PictureService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });
            services.AddHttpClient<GalleryService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });
            services.AddHttpClient<EventService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });
            services.AddHttpClient<ConnectionService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });
            services.AddHttpClient<CharacterService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });
            services.AddHttpClient<BookService>(client => { client.BaseAddress = new Uri("http://localhost:5153/"); });

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<ITimelineService, TimelineService>();
            services.AddScoped<IUserService, UserService>();
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
