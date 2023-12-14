using AdressBook.ConsoleApp.Services;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdressBook.Shared.Repositories;
using AdressBook.Shared.Interfaces;



//spara in dessa i listan
//ContactService contactService = new();


//hämta menuservis instansiering och skicka med contactService för att komma åt dess metoder
//MenuService menuService = new MenuService(contactService);

//implementera Dependency Injection, välj application
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    //De klasser jag skickat med i en konstruktor registreras här
    services.AddSingleton<IFileService>(new FileService(@"C:\Projects\content.json"));
    services.AddSingleton<ContactRepository>();
    services.AddSingleton<ContactService>();
    services.AddSingleton<MenuService>();



}).Build();

builder.Start();
Console.Clear();

var menuService = builder.Services.GetRequiredService<MenuService>();
//hämta utiåfrån Builder-delen
menuService.ShowMenu();


