using AdressBook.ConsoleApp.Services;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdressBook.Shared.Repositories;
using AdressBook.Shared.Interfaces;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<FileService>(new FileService(@"C:\Projects\content.json"));
    services.AddSingleton<ContactRepository>();
    services.AddSingleton<ContactService>();
    services.AddSingleton<MenuService>();

}).Build();

builder.Start();
Console.Clear();

var menuService = builder.Services.GetRequiredService<MenuService>();

menuService.ShowMenu();


