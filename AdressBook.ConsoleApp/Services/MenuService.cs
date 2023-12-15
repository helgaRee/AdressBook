using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdressBook.ConsoleApp.Services;

public class MenuService
{
    //Gör instansiering av klassen ContactService för att anvädna dess funktioner här
    private readonly ContactService _contactService = new ContactService();

    //Metod - visar användarmenyn
    public void ShowMenu()
    {


        while (true)
        {
            Console.Clear();
            Console.WriteLine("## ADRESS BOOK ##");
            Console.WriteLine("");
            Console.WriteLine("Välj bland alternativen in menyn nedan.");
            Console.WriteLine($"{"1.",-3} Lägg till kontakt");
            Console.WriteLine($"{"2.",-3} Visa specifik kontakt");
            Console.WriteLine($"{"3.",-3} Visa alla kontakter");
            Console.WriteLine($"{"4.",-3} Ta bort en kontakt");
            Console.WriteLine($"{"5.",-3} Avsluta programmet");
            Console.WriteLine("");
            Console.WriteLine("Tryck enter för att gå vidare");

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
                    ShowAllContacts();
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


    public void AddContact()
    {
        DisplayTitle("Lägg till ny kontakt");

        Console.Write("Ange förnamn: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Ange efternamn: ");
        string lastName = Console.ReadLine()!;
        Console.Write("Ange email: ");
        string email = Console.ReadLine()!;
        Console.Write("Ange mobilnummer: ");
        string phoneNumber = Console.ReadLine()!;
        Console.Write("Ange gatuadress: ");
        string streetAdress = Console.ReadLine()!;
        Console.Write("Ange postkod: ");
        string postalCode = Console.ReadLine()!;
        Console.Write("Ange stad: ");
        string city = Console.ReadLine()!;


        // Skapa en ny kontakt med de användarinmatade värdena
        var contact = new Contact(firstName, lastName, email, streetAdress, city, postalCode, phoneNumber);

        //lägg till i listan
        _contactService.AddContactToList(contact);

        //Ge bekräftelse till användaren
        Console.WriteLine("");
        Console.WriteLine($"{firstName} {lastName} har lagts till som ny kontakt till adressboken.");
        Console.WriteLine("");
        Console.WriteLine("Tryck enter för att gå vidare");
    }





    public void ShowSpecificContact()
    {
        DisplayTitle("Öppna en kontakt");
        //hämta listan
        var contactList = _contactService.GetContactsFromList();

        if (contactList.Count > 0)
        {
            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Namn: {contactList[i].FirstName} {contactList[i].LastName}");
            }

            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= contactList.Count)
            {
                // Hämta den valda kontakten
                var selectedContact = contactList.ElementAt(selectedIndex - 1);
                Console.Clear();
                // Visa detaljer om den valda kontakten
                Console.WriteLine("");
                Console.WriteLine($"{"##",-3}Namn: {selectedContact.FirstName} {selectedContact.LastName}");
                Console.WriteLine($"{"-",-3}Email: {selectedContact.Email}");
                Console.WriteLine($"{"-",-3}Adress: {selectedContact.Address}");
                Console.WriteLine($"{"-",-3}Postkod: {selectedContact.PostalCode}");
                Console.WriteLine($"{"-",-3}Stad: {selectedContact.City}");
                Console.WriteLine($"{"-",-3}Mobilnummer: {selectedContact.PhoneNumber}");
                Console.WriteLine("");
                Console.WriteLine("Vill du ändra kontakten? (ja/nej)");
                Console.WriteLine("");
                string userChoiceChange = Console.ReadLine()?.ToLower() ?? "";
                

                if (userChoiceChange == "ja")
                {
                    ChangeContact(selectedContact);
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }
        }
        else
        {
            Console.WriteLine("Det finns inga kontakter i adressboken.");
        }


    }

    public void ChangeContact(IContact selectedContact)
    {
        DisplayTitle("Ändra din kontakt");
        while (true)
        {
            // Visa detaljer om den valda kontakten
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"{"1",-3}. Förnamn: {selectedContact.FirstName}");
            Console.WriteLine($"{"2",-3}. Efteramn: {selectedContact.LastName}");
            Console.WriteLine($"{"3",-3}. Email: {selectedContact.Email}");
            Console.WriteLine($"{"4",-3}. Adress: {selectedContact.Address}");
            Console.WriteLine($"{"5",-3}. Postkod: {selectedContact.PostalCode}");
            Console.WriteLine($"{"6",-3}. Stad: {selectedContact.City}");
            Console.WriteLine($"{"7",-3}. Mobilnummer: {selectedContact.PhoneNumber}");

            //Be användaren om nya uppgifter

            Console.WriteLine("");
            Console.WriteLine("Ange siffra för vilken rad du vill ändra (1-7)");
            Console.WriteLine("");
            Console.WriteLine("Ange 'meny' för att gå tillbaka.");
            string userChoice = Console.ReadLine() ?? "";

            if (userChoice.ToLower() == "meny")
            {
                break;
                Console.Clear();
            }

            switch (userChoice)
            {
                case "1":
                    Console.Write("Ange nytt förnamn: ");
                    string newFirstName = Console.ReadLine();
                    selectedContact.FirstName = newFirstName;
                    break;
                case "2":
                    Console.Write("Ange nytt efternamn: ");
                    string newLastName = Console.ReadLine();
                    selectedContact.LastName = newLastName;
                    break;
                case "3":
                    Console.Write("Ange ny email: ");
                    string newEmail = Console.ReadLine();
                    selectedContact.Email = newEmail;
                    break;
                case "4":
                    Console.Write("Ange ny adress: ");
                    string newAddress = Console.ReadLine();
                    selectedContact.Address = newAddress;
                    break;
                case "5":
                    Console.Write("Ange ny postkod: ");
                    string newPostalCode = Console.ReadLine();
                    selectedContact.PostalCode = newPostalCode;
                    break;
                case "6":
                    Console.Write("Ange ny stad: ");
                    string newCity = Console.ReadLine();
                    selectedContact.City = newCity;
                    break;
                case "7":
                    Console.Write("Ange nytt mobilnummer: ");
                    string newPhoneNumber = Console.ReadLine();
                    selectedContact.PhoneNumber = newPhoneNumber;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        // Uppdatera kontaktlistan
        _contactService.UpdateContact((Contact)selectedContact);

        Console.WriteLine("Kontakten har uppdaterats.");
        Console.ReadKey(); 


    }

    public void ShowAllContacts()
    {
        DisplayTitle("Alla kontakter i Adressboken");

        //hämta listan
        var contactList = _contactService.GetContactsFromList();

        foreach (var contact in contactList)
        {
            Console.WriteLine($"{" *",-3} Namn:{contact.FirstName} {contact.LastName}\n {"-",-3}Email: {contact.Email}");
            Console.WriteLine("\n");
        }

            Console.WriteLine("Ange Delete för att ta bort en kontakt, eller show för att öppna och se detaljer. Eller tryck enter för att gå tillbaka till menyn.");
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
                Console.WriteLine("Du skickas tillbaka till menyn.");
                Console.ReadKey();
                break;
        }         
    }


    public void DeleteContact()
    {
        DisplayTitle("Ta bort en kontakt ur listan");

        // Hämta befintliga kontakter från filen
        var contactList = _contactService.GetContactsFromList();

        // Visa kontakter om det finns några
        if (contactList.Any())
        {
            Console.WriteLine("Befintliga kontakter");
            Console.WriteLine("\n");
            foreach (var contact in contactList)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName} {contact.Email}");
            }

            //be anv ange epost för den som ska tas bort
            Console.WriteLine("\n");
            Console.WriteLine("Ange email för vilken kontakt du vill ta bort");
          //Gör en kontroll, om input inte finns, ge felmeddelande, annars gå vdare



            string emailToDelete = Console.ReadLine();
            if (emailToDelete != "")
            {
            //spara undan informationen om kontakten, INNAN den taas bort, med FirstOrDefault()
            var contactToRemove = contactList.FirstOrDefault(contact => contact.Email.Equals(emailToDelete, StringComparison.OrdinalIgnoreCase));


            //anropa metod för att ta bort från listan
            Console.Clear();
            bool deleteSuccess = Convert.ToBoolean(_contactService.DeleteContactFromList(emailToDelete));

            //ge bekräftelse på att Delete fungerade
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
                Console.WriteLine("Felaktig inmatning, du skickas tillbaka till huvudmenyn..");
            }
        }
    }


    public void ExitProgram()
    {
        DisplayTitle("Stäng av programmet");

        Console.WriteLine("Är du säker på att du vill avsluta? (ja/nej)");
        string userOption = Console.ReadLine()?.ToLower();

        if (userOption != "ja")
        {
            Console.WriteLine("Tryck enter för att gå tillbaka till huvudmenyn.");
        }
        if (userOption != "ja" && userOption != "nej")
        {
            Console.Clear();
            Console.WriteLine("Ops, nu blev det fel.. du skickas tillbaka till menyn.");
        }
        else
        {
            Environment.Exit(0);
        }

    }

    //Metod för titel
    public void DisplayTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"### {title} ###");
        Console.WriteLine("");
    }

    //Metod för att återgå till meny
    public void ReturnToMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("Ange 'meny' för att gå tillbaka.");
        string userChoice = Console.ReadLine() ?? "";

        if (userChoice.ToLower() == "meny")
        {
            Console.ReadKey();
            Console.WriteLine("Du skickas tillbaka till huvudmenyn.");
        }
    }
}