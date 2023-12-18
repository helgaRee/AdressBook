using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdressBook.ConsoleApp.Services;
//Menuservice - managing menu with input from user
public class MenuService
{
    //Gör instansiering av klassen ContactService för att anvädna dess funktioner här
    private readonly ContactService _contactService = new ContactService();

    /// <summary>
    /// Show main menu
    /// </summary>
    /// <param name=""></param>
    /// <returns>Allow user to wiew the mainmenu and to navigate to methods.</returns>
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"{"",-50}### ADRESS BOOK ###");
            Console.WriteLine("");
            Console.WriteLine("Välj bland alternativen in menyn nedan:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("");
            Console.WriteLine($"{"1.",-3} Lägg till kontakt");
            Console.WriteLine($"{"2.",-3} Visa specifik kontakt");
            Console.WriteLine($"{"3.",-3} Visa alla kontakter");
            Console.WriteLine($"{"4.",-3} Ta bort en kontakt");
            Console.WriteLine($"{"5.",-3} Avsluta programmet");
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Ange siffra och tryck enter för att gå vidare");

            var userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ShowSpecificContact();
                    break;
                case "3":
                    ShowContacts();
                    break;
                case "4":
                    DeleteContact();
                    break;
                case "5":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Add new contact
    /// </summary>
    /// <param name=""></param>
    /// <returns>Allow user to add new contact to list.</returns>
    public void AddContact()
    {
        DisplayTitle("Lägg till ny kontakt");

        Console.Write($"{"",-4}Ange förnamn: ");
        string firstName = Console.ReadLine()!;
        Console.Write($"{"",-4}Ange efternamn: ");
        string lastName = Console.ReadLine()!;
        Console.Write($"{"",-4}Ange email: ");
        string email = Console.ReadLine()!;
        Console.Write($"{"",-4}Ange mobilnummer: ");
        string phoneNumber = Console.ReadLine()!;
        Console.Write($"{"",-4}Ange gatuadress: ");
        string streetAdress = Console.ReadLine()!;
        Console.Write($"{"",-4}Ange postkod: ");
        string postalCode = Console.ReadLine()!;
        Console.Write($"{"",-4}Ange stad: ");
        string city = Console.ReadLine()!;

        var contact = new Contact(firstName, lastName, email, streetAdress, city, postalCode, phoneNumber);

        _contactService.AddContactToList(contact);

        Console.WriteLine("");
        Console.WriteLine($"{"", -4}{firstName} {lastName} har lagts till som ny kontakt till adressboken.");
        Console.WriteLine("");
        Console.WriteLine($"{"",-4}Tryck enter för att gå vidare");
    }

    /// <summary>
    /// Show details about a specific contact in list
    /// </summary>
    /// <param name=""></param>
    /// <returns>Allow user to wiew the details of a specific contact.</returns>
    public void ShowSpecificContact()
    {
        DisplayTitle("Öppna en kontakt");
 
        var contactList = _contactService.GetContactsFromList();

        Console.WriteLine("Ange siffran för den kontakt du vill visa");
        Console.WriteLine("");

        if (contactList.Count > 0)
        {
            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Namn: {contactList[i].FirstName} {contactList[i].LastName}");
            }

            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= contactList.Count)
            {
                var selectedContact = contactList.ElementAt(selectedIndex - 1);
                Console.Clear();

                DisplayTitle($"Kontakt {selectedIndex} {selectedContact.FirstName} {selectedContact.LastName}");
                Console.WriteLine("");
                //Console.WriteLine($"{"##",-3}Namn: {selectedContact.FirstName} {selectedContact.LastName}");
                Console.WriteLine($"{"-",-4}Email: {selectedContact.Email}");
                Console.WriteLine($"{"-",-4}Adress: {selectedContact.Address}");
                Console.WriteLine($"{"-",-4}Postkod: {selectedContact.PostalCode}");
                Console.WriteLine($"{"-",-4}Stad: {selectedContact.City}");
                Console.WriteLine($"{"-",-4}Mobilnummer: {selectedContact.PhoneNumber}");
                Console.WriteLine("");
                Console.WriteLine($"{"",-4}Vill du ändra kontakten? (ja/nej)");
                Console.WriteLine("");
                string userChoiceChange = Console.ReadLine()?.ToLower() ?? "";
                
                if (userChoiceChange == "ja")
                {
                    ChangeContact(selectedContact);
                }
                else if(userChoiceChange == "nej")
                {
                    Console.Clear();
                    ReturnToMenu();
                }
            }
            else
            {
                Console.WriteLine($"{"",-4}Ogiltigt val. Försök igen.");
            }
        }
        else
        {
            Console.WriteLine($"{"",-4}Det finns inga kontakter i adressboken.");
        }
    }

    /// <summary>
    /// Change details about an existing contact in list
    /// </summary>
    /// <param name="selectedContact">The selected contact by user, type of a string</param>
    /// <returns>An update of the chosen contact by user</returns>
    public void ChangeContact(IContact selectedContact)
    {
        DisplayTitle("Ändra kontakt");
        while (true)
        {         
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"{"1",-4}. Förnamn: {selectedContact.FirstName}");
            Console.WriteLine($"{"2",-4}. Efteramn: {selectedContact.LastName}");
            Console.WriteLine($"{"3",-4}. Email: {selectedContact.Email}");
            Console.WriteLine($"{"4",-4}. Adress: {selectedContact.Address}");
            Console.WriteLine($"{"5",-4}. Postkod: {selectedContact.PostalCode}");
            Console.WriteLine($"{"6",-4}. Stad: {selectedContact.City}");
            Console.WriteLine($"{"7",-4}. Mobilnummer: {selectedContact.PhoneNumber}");

            Console.WriteLine("");
            Console.WriteLine($"{"",-4}Ange siffra för vilken rad du vill ändra (1-7)");
            Console.WriteLine("");
            Console.WriteLine($"{"",-4}Ange 'meny' för att gå tillbaka.");
            string userChoice = Console.ReadLine() ?? "";

            if (userChoice.ToLower() == "meny")
            {
                break;
                Console.Clear();
            }

            switch (userChoice)
            {
                case "1":
                    Console.Write($"{"",-4}Ange nytt förnamn: ");
                    string newFirstName = Console.ReadLine();
                    selectedContact.FirstName = newFirstName;
                    break;
                case "2":
                    Console.Write($"{"",-4}Ange nytt efternamn: ");
                    string newLastName = Console.ReadLine();
                    selectedContact.LastName = newLastName;
                    break;
                case "3":
                    Console.Write($"{"",-4}Ange ny email: ");
                    string newEmail = Console.ReadLine();
                    selectedContact.Email = newEmail;
                    break;
                case "4":
                    Console.Write($"{"",-4}Ange ny adress: ");
                    string newAddress = Console.ReadLine();
                    selectedContact.Address = newAddress;
                    break;
                case "5":
                    Console.Write($"{"",-4}Ange ny postkod: ");
                    string newPostalCode = Console.ReadLine();
                    selectedContact.PostalCode = newPostalCode;
                    break;
                case "6":
                    Console.Write($"{"",-4}Ange ny stad: ");
                    string newCity = Console.ReadLine();
                    selectedContact.City = newCity;
                    break;
                case "7":
                    Console.Write($"{"",-4}Ange nytt mobilnummer: ");
                    string newPhoneNumber = Console.ReadLine();
                    selectedContact.PhoneNumber = newPhoneNumber;
                    break;
                default:
                    Console.WriteLine($"{"",-4}Ogiltigt val.");
                    break;
            }
        }
        _contactService.UpdateContact((Contact)selectedContact);
        Console.Clear();
        Console.WriteLine($"Kontakten har uppdaterats. Tryck enter för att återgå till huvudmenyn.");
    }

    /// <summary>
    /// Show all contacts that's saved in the list
    /// </summary>
    /// <param name=""></param>
    /// <returns>A list with all asved contacts</returns>
    public void ShowContacts()
    {
        DisplayTitle("Sparade kontakter i Addressboken");

        var contactList = _contactService.GetContactsFromList();

        if (contactList.Any())
        {
            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Namn:  {contactList[i].FirstName} {contactList[i].LastName}\n{"", -3}Email: {contactList[i].Email}");
                Console.WriteLine("\n");
            }
                Console.WriteLine($"Ange 'Delete' för att ta bort, 'Show' för att visa kontakt\neller tryck enter för att gå tillbaka till menyn.");
                string userChoice = Console.ReadLine();

        switch(userChoice.ToLower())
        {
            case "delete":
                DeleteContact();
                Console.ReadKey();
                break;
            case "show":
                ShowSpecificContact();
                Console.ReadKey();
                break;
            default:
                Console.WriteLine($"Du skickas tillbaka till menyn.");
                break;
        }         
        }
        else
        {
            Console.WriteLine($"Det finns inga kontakter i addressboken.\nTryck enter för att återgå till huvudmenyn.");
        }
    }

    /// <summary>
    /// Delete a contact from list
    /// </summary>
    /// <param name=""></param>
    /// <returns>If succed, a list with the chosen contact deleted, if false no change.</returns>
    public void DeleteContact()
    {
        DisplayTitle("Ta bort en kontakt ur listan");

        var contactList = _contactService.GetContactsFromList();

        if (contactList.Any())
        {
            Console.WriteLine("Befintliga kontakter");
            Console.WriteLine("\n");

            foreach (var contact in contactList)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName} {contact.Email}");
            }

            Console.WriteLine("\n");
            Console.WriteLine($"Ange email för vilken kontakt du vill ta bort");
            string emailToDelete = Console.ReadLine();

            if (emailToDelete != "")
            {
            var contactToRemove = contactList.FirstOrDefault(contact => contact.Email.Equals(emailToDelete, StringComparison.OrdinalIgnoreCase));

            Console.Clear();
            bool deleteSuccess = Convert.ToBoolean(_contactService.DeleteContactFromList(emailToDelete));

            if (deleteSuccess)
            {
                Console.WriteLine($"Kontakt med e-postadress {contactToRemove.Email} har tagits bort.");
            }
            else
            {
                {
                    Console.WriteLine($"Ingen kontakt med e-postadressen {contactToRemove.Email} hittades.");
                }
            }
            }
            else
            {
                Console.WriteLine($"Felaktig inmatning, du skickas tillbaka till huvudmenyn..");
            }
        }
        else
        {
            Console.WriteLine($"Det finns inga kontakter i addressboken.\nTryck enter för att återgå till huvudmenyn.");
        }
    }

    /// <summary>
    /// Exit the program
    /// </summary>
    /// <param name=""></param>
    /// <returns>If true, program exists, if false back to main menu</returns>
    public void ExitProgram()
    {
        DisplayTitle("Stäng av programmet");

        Console.WriteLine($"Är du säker på att du vill avsluta? (ja/nej)");
        string userOption = Console.ReadLine()?.ToLower();

        if (userOption != "ja")
        {
            Console.WriteLine($"Tryck enter för att gå tillbaka till huvudmenyn.");
        }
        if (userOption != "ja" && userOption != "nej")
        {
            Console.Clear();
            Console.WriteLine($"Ops, nu blev det fel.. du skickas tillbaka till menyn.");
        }
        else
        {
            Environment.Exit(0);
        }

    }

    /// <summary>
    /// Display title for each method
    /// </summary>
    /// <param name="title">Specific title, type of a string</param>
    /// <returns>A specific title for each method</returns>
    public void DisplayTitle(string title)
    {
        Console.Clear();
        Console.WriteLine("");
        Console.WriteLine($"{"",-50}### ADRESS BOOK ###");
        Console.WriteLine("");
        Console.WriteLine($"## {title} ##");
        Console.WriteLine("---------------------------------------");
        Console.WriteLine("");
    }

    /// <summary>
    /// Return to main menu
    /// </summary>
    /// <param name=""></param>
    /// <returns>Returning to main menu</returns>
    public void ReturnToMenu()
    {
        Console.WriteLine("");
        Console.WriteLine($"Ange 'meny' för att gå tillbaka.");
        string userChoice = Console.ReadLine() ?? "";

        if (userChoice.ToLower() == "meny")
        {
           // Console.ReadKey();
            Console.WriteLine($"Du skickas tillbaka till huvudmenyn.");
        }
    }
}