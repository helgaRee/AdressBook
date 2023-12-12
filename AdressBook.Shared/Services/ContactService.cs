using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdressBook.Shared.Services;

public class ContactService
{
    //Specificerar vart sökvägen ska gå och skapar listan
    private readonly FileService _fileService = new FileService(@"C:\EC\Projects\content.json");
    private List<Contact> _contactList = new List<Contact>();


    //hämtar denna metoden när jag vill lägga till något till listan
    public void AddContactToList(Contact contact)
    {
        try
        {
        //kontrollera om det inte finns en kontakt i listan, finns ingen, lägg till
        if (!_contactList.Any(currentContactInList => currentContactInList.Email == contact.Email))
        {
            _contactList.Add(contact);
            _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
        }
        }
        catch ( Exception ex ) { Debug.WriteLine(ex); };
    }





    public void DeleteContactFromList(string email)
    {
        {
            try
            {
                // Hämta befintliga kontakter
                var contactList = GetContactsFromList().ToList();

                // Hitta kontakten med den angivna e-postadressen
                var contactToDelete = contactList.FirstOrDefault(contact => contact.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (contactToDelete != null)
                {
                    // Ta bort kontakten från listan
                    contactList.Remove(contactToDelete);

                    // Spara den uppdaterade listan till filen
                    _fileService.SaveContactToFile(JsonConvert.SerializeObject(contactList));

                    Console.WriteLine($"Kontakt med e-postadress {email} har tagits bort.");
                }
                else
                {
                    Console.WriteLine($"Ingen kontakt med e-postadressen {email} hittades.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
    }





    //Hämta LISTAN med en IEnumerable! Men varför? varför inte en vanlig lista?
    //sätter den till IEnumerable så att den endast blir LÄSBAR, får ej adda/tabort något från listan

    public List<Contact> GetContactsFromList()
    {
        try
        { //Hämta contacten
            var contact = _fileService.GetContactFromFile();
            //gör om listan till json. Om strängen inte är null/empty
            if (!string.IsNullOrEmpty(contact))
            {

               _contactList = JsonConvert.DeserializeObject<List<Contact>>(contact)!;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); };
        //oavsett vad
        return _contactList;
    }


}
