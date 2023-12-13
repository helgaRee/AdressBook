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

    //Konstruktor
    public MenuService(ContactService contactsService)
    {
        _contactService= contactsService;
    }

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

        //Rensa och Ge bekräftelse till användaren
        Console.Clear();
        Console.WriteLine($"{firstName} {lastName} har lagts till som ny kontakt till adressboken.");
        Console.WriteLine("");
        Console.WriteLine("Tryck enter för att gå vidare");
    }



public void DeleteContact()
{
            DisplayTitle("Ta bort en kontakt ur listan");

            // Hämta befintliga kontakter från filen
            var contactList = _contactService.GetContactsFromList(_contactService.Get_contactList()).ToList();

            // Visa kontakter om det finns några
            if (contactList.Any())
            {
            Console.WriteLine("Befintliga kontakter");
            Console.WriteLine("\n");
            foreach(var contact in contactList)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName} {contact.Email}");
            }

            //be anv ange epost för den som ska tas bort
            Console.WriteLine("\n");
            Console.WriteLine("Ange email för vilken kontakt du vill ta bort");
            string emailToDelete = Console.ReadLine();

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
    }


    public void ShowSpecificContact()
    {
        DisplayTitle("Öppna en kontakt");
        //hämta listan
       var contactList = _contactService.GetContactsFromList(_contactService.Get_contactList());

        if (contactList.Count > 0)
        {
            for( int i = 0; i < contactList.Count; i++ )
            {
                Console.WriteLine($"{i + 1}. Kontakt: {contactList[i].FirstName} {contactList[i].LastName}");
            } 
              
          if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= contactList.Count)
            {
                // Hämta den valda kontakten
                var selectedContact = contactList.ElementAt(selectedIndex - 1);

                // Visa detaljer om den valda kontakten
                Console.WriteLine($"Namn: {selectedContact.FirstName} {selectedContact.LastName}");
                Console.WriteLine($"Email: {selectedContact.Email}");
                Console.WriteLine($"Adress: {selectedContact.Address}");
                Console.WriteLine($"Postkod: {selectedContact.PostalCode}");
                Console.WriteLine($"Stad: {selectedContact.City}");
                Console.WriteLine($"Mobilnummer: {selectedContact.PhoneNumber}");
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
        Console.WriteLine("");
        Console.WriteLine("Tryck enter för att gå vidare");
    }





    public void ShowContacts()
    {
        DisplayTitle("Visa alla kontakter");

        //hämta listan
        var contactList = _contactService.GetContactsFromList(_contactService.Get_contactList());

        foreach (var contact in contactList)
        {
            Console.WriteLine($"{"-", -3} Namn:{contact.FirstName} {contact.LastName}");
     //       Console.WriteLine($"{"-",-3} Namn:{contact.FirstName} {contact.LastName}\n {"-",-3}Email: {contact.Email}\n {"-",-3}Adress: " +
   // $"{contact.Address}\n {"-",-3}Postkod: {contact.PostalCode} {"-",-3}stad: {contact.City}\n {"-",-3}Mobilnummer: {contact.PhoneNumber}");
            Console.WriteLine("\n");
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

