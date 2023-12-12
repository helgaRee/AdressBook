using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdressBook.Shared.Services;
//I contactService kan jag lägga till, ta bort och hämta kontakter. Och uppdatera?
public class ContactService
{

    private readonly FileService _fileService = new FileService(@"C:\EC\Projects\content.json");
    private List<Contact> _contactList = new List<Contact>();


    //Add contacts to list
    public void AddContactToList(Contact contact)
    {
        try
        {
        if (!_contactList.Any(currentContactInList => currentContactInList.Email == contact.Email))
        {
            _contactList.Add(contact);
            _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
        }
        }
        catch ( Exception ex ) { Debug.WriteLine(ex); };
    }




    //Delete contacts from list
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


    //Get and read contactList
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
