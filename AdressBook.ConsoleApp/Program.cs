﻿using AdressBook.ConsoleApp.Services;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;



//spara in dessa i listan
ContactService contactService = new();


//hämta menuservis instansiering
MenuService menuService = new MenuService();

menuService.ShowMenu();