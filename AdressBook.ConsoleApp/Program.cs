using AdressBook.ConsoleApp.Services;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;



//spara in dessa i listan
ContactService contactService = new();

//hämta menuservis instansiering och skicka med contactService för att komma åt dess metoder
MenuService menuService = new MenuService(contactService);

menuService.ShowMenu();