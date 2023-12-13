//I contactService kan jag lägga till, ta bort och hämta kontakter. Och uppdatera?
using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdressBook.Shared.Services;
public class ContactService : IContactService
{

    private readonly FileService _fileService = new FileService(@"C:\EC\Projects\content.json");
    private List<IContact> _contactList = new List<IContact>();


    //Add contacts to list
    public bool AddContactToList(IContact contact)
    {
        try
        {
        if (!_contactList.Any(currentContactInList => currentContactInList.Email == contact.Email))
        {
            _contactList.Add(contact);
            _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
                return true;
        }
        }
        catch ( Exception ex ) { Debug.WriteLine(ex.Message); }
        return false;
    }




    //Delete contacts from list, bool to return a value
    public bool DeleteContactFromList(string email)
    {
        
        try
        {
            // Hämta befintliga kontakter
            var contactList = GetContactsFromList(Get_contactList()).ToList();

            // Hitta kontakten med den angivna e-postadressen
            var contactToDelete = contactList.FirstOrDefault(contact => contact.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (contactToDelete != null)
            {
                // Ta bort kontakten från listan
                contactList.Remove(contactToDelete);

                // Spara den uppdaterade listan till filen
                _fileService.SaveContactToFile(JsonConvert.SerializeObject(contactList));

                return true;
            }
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex); 
        };
        return false;
    }

    public List<IContact> Get_contactList()
    {
        return _contactList;
    }



    //Get and read contactList
    public List<IContact> GetContactsFromList(List<IContact> _contactList)
    {
        try
        { //Hämta contacten
            var contact = _fileService.GetContactFromFile();
            //gör om listan till json. Om strängen inte är null/empty
            if (!string.IsNullOrEmpty(contact))
            {

               _contactList = JsonConvert.DeserializeObject<List<IContact>>(contact)!;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); };
        //oavsett vad
        return _contactList;
    }

    //för testning
    public IEnumerable<IContact> GetAllFromList()
    {
        try
        {
            return _contactList;
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return null!;
        
    }

    public bool DeleteContactFromList(IContact contact)
    {
        throw new NotImplementedException();
    }
}
