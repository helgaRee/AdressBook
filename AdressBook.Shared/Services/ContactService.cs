using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AdressBook.Shared.Services;

public class ContactService
{
    //Specificerar vart sökvägen ska gå och skapar listan
    private readonly FileService _fileService = new FileService(@"C:\EC\Projects\content.json");
    private List<Contact> _contactList = new List<Contact>();

    //skapar en createUser tex
    //void om du inte vill få ett meddelande tillbaka
    public void AddContactToList(Contact contact)
    {
        try
        {

        //kontrollera om det inte finns en kontakt i listan, finns ingen, lägg till
        if (!_contactList.Any(currentContactInList => currentContactInList.Email == contact.Email))
        {
            //konv om listan till JSON och serializerar min lista
            _contactList.Add(contact);
            _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
        }
        }
        catch ( Exception ex ) { Debug.WriteLine(ex); };
        //returnerar ingenting
    }



    //Hämta LISTAN med en IEnumerable! Men varför? varför inte en vanlig lista?
    //sätter den till IEnumerable så att den endast blir LÄSBAR, får ej adda/tabort något från listan

    public IEnumerable<Contact> GetContactsFromList()
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
