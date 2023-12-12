using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using System.Runtime.CompilerServices;

namespace AdressBook.Console.Services;

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
        DisplayTitle("Visa detaljer om en kontakt");
        //hämta specifik kontakt med indexering
        var contactList = _contactService.GetContactsFromList();

        //OM det finns någon i listan, gå vidare
        if (contactList.Count > 0)
        {
            //Visa listan
             Console.WriteLine("Välj en kontakt: ");
               for(int i = 0; i < contactList.Count; i++)
                {
                Console.WriteLine($"{i + 1}. {contactList[i].FirstName} {contactList[i].LastName}");
                }

               //Läs in användarens svar
             if (int.TryParse(Console.ReadLine(), out int selectedIndex))
            {
                //kontrollera om det valda indexet är inom rätt intervall
                if(selectedIndex >= 1 && selectedIndex <= contactList.Count)
                {
                    //justera index för att kunna hämta rätt kontakt
                    var selectedContact = contactList[selectedIndex - 1];
                    Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}\n Email: {contact.Email}\n Adress: " +
                    $"{contact.Address}\n Postkod: {contact.PostalCode} stad: {contact.City}\n {contact.PhoneNumber}");
                }
                else
                {
                    Console.WriteLine("Det valda kontakten verkar inte finnas, försök igen..!");
                }
            }

        }
        else
        {
            Console.WriteLine("Det finns inga kontakter i adressboken. Gå tillbaka till huvudmenyn för att lägga till nya kontakter.");
        }
    }


    public void ShowAllContacts()
    {
        DisplayTitle("Visa alla kontakter");

        //hämta listan
        var contactList = _contactService.GetContactsFromList();

        foreach (var contact in contactList)
          {
            Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}\n Email: {contact.Email}\n Adress: " +
                $"{contact.Address}\n Postkod: {contact.PostalCode} stad: {contact.City}\n {contact.PhoneNumber}");
        }


    }



    public void DeleteContact()
    {
        DisplayTitle("Ta bort en kontakt ur listan");
    }
    public void ExitProgram()
    {
        DisplayTitle("Stäng av programmet");
    }

    //Metod för titel
    public void DisplayTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"{title}");
        Console.WriteLine("");
    }
}
