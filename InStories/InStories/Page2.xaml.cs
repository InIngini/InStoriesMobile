﻿using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.Errors;
using InStories.Core.Services.Interfaces;
using InStories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InStories.Common;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;
using InStories.Core.DB.Interfaces;
using InStories.Core.Errors;
using InStories.Core.Services.Interfaces;

namespace InStories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IBookService BookService { get; set; }
        private IUserService UserService { get; set; }

        private int BookId;
        public Page2(IServiceProvider serviceProvider, int id)
        {
            ServiceProvider = serviceProvider;
            BookService = ServiceProviderServiceExtensions.GetService<IBookService>(ServiceProvider);
            UserService = ServiceProviderServiceExtensions.GetService<IUserService>(ServiceProvider);

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
            var book = await BookService.GetBook(BookId);
            var nameBook = book != null ? book.NameBook : "Новая книга";
            // Обновление элементов управления
            nameLabel.Text = nameBook.Length > 12 ? nameBook.Substring(0, 12) + "..." : nameBook;
            textEditor.Text = nameBook; // Установка текста в редактор
        }

        private async Task<int> SaveBook()
        {
            var foundEditor = buttonsGrid.Children
                    .FirstOrDefault(x => x is Editor && ((Editor)x).AutomationId == "BookName") as Editor;
            Book book = new Book();
            if (BookId == 0)
            {
                var userBook = new UserBookData()
                {
                    UserId = (await UserService.GetUser()).Id,
                    NameBook = foundEditor.Text
                };
                try
                {
                    book = await BookService.CreateBook(userBook);
                    return book.Id;
                }
                catch (Exception ex)
                {
                    var errorMessage = await ErrorsDeserialization.Deserialization(ex);
                    await DisplayAlert("Ошибка", errorMessage, "OK");
                }
            }
            else
            {
                book.Id = BookId;
                book.NameBook = foundEditor.Text;

                try
                {
                    await BookService.UpdateBook(BookId, book);
                }
                catch (Exception ex)
                {
                    var errorMessage = await ErrorsDeserialization.Deserialization(ex);
                    await DisplayAlert("Ошибка", errorMessage, "OK");
                }
            }
            return book.Id;
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await SaveBook();
            await Navigation.PushAsync(new Page1(ServiceProvider));
        }

        private async void ButtonHome_Clicked(object sender, EventArgs e)
        {
            var id = await SaveBook();
            await Navigation.PushAsync(new Page2(ServiceProvider, id));
        }
        private async void ButtonPersona_Clicked(object sender, EventArgs e)
        {
            var id = await SaveBook();
            await Navigation.PushAsync(new Page3(ServiceProvider, id));
        }
        private async void ButtonShema_Clicked(object sender, EventArgs e)
        {
            var id = await SaveBook();
            await Navigation.PushAsync(new Page4(ServiceProvider, id));
        }
        private async void ButtonTime_Clicked(object sender, EventArgs e)
        {
            var id = await SaveBook();
            await Navigation.PushAsync(new Page5(ServiceProvider, id));
        }
    }
}