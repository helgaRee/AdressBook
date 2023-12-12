using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
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
        Console.WriteLine($"{firstName} {lastName} har lagts till som ny kontakt till adressboken.");
        Console.WriteLine("Tryck enter för att gå vidare");
    }





    public void ShowSpecificContact()
    {
       
    }





    public void ShowAllContacts()
    {
        DisplayTitle("Visa alla kontakter");

        //hämta listan
        var contactList = _contactService.GetContactsFromList();

        foreach (var contact in contactList)
        {
            Console.WriteLine($" Namn: {contact.FirstName} {contact.LastName}\n Email: {contact.Email}\n Adress: " +
                $"{contact.Address}\n Postkod: {contact.PostalCode} stad: {contact.City}\n Mobilnummer: {contact.PhoneNumber}");
            Console.WriteLine("\n\n");
        }


    }






public void DeleteContact()
{
    DisplayTitle("Ta bort en kontakt ur listan");

        // Hämta befintliga kontakter från filen
        var contactList = _contactService.GetContactsFromList().ToList();

        // Visa kontakter om det finns några
        if (contactList.Any())
        {
            Console.WriteLine("Befintliga kontakter");
            foreach(var contact in contactList)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName} {contact.Email}");
            }

            //be anv ange epost för den som ska tas bort
            Console.WriteLine("Ange email för vilken kontakt du vill ta bort");
            string emailToDelete = Console.ReadLine();

            //anropa metod för att ta bort från listan
        _contactService.DeleteContactFromList(emailToDelete);
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
            Console.WriteLine($"{title}");
            Console.WriteLine("");
        }
    } 

