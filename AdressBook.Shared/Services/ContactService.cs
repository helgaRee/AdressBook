//## HANTERING AV LISTAN - lägga till, ta bort, uppdatera, visa listan ##

using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using AdressBook.Shared.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AdressBook.Shared.Services;
public class ContactService : IContactService
{
    private readonly IFileService _fileService; 
    private List<IContact> _contactList = new List<IContact>();
    
    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = GetContactsFromList();
    }

    private List<IContact> GetContactsFromList()
    {
        throw new NotImplementedException();
    }

    public ContactService()
    {
        // Konstruktorlogik utan några beroenden
    }

    /// <summary>
    /// Add a contact to a contact List
    /// </summary>
    /// <param name="contact">A contact of type IContact</param>
    /// <returns>Returns true if successfull, or false if it failes or contact already exists</returns>
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
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }



    /// <summary>
    /// Delete a contact from a contact List
    /// </summary>
    /// <param name="email">An email of type string</param>
    /// <returns>Returns true if delete was succesfull, or false if it failes or the email doesnt exist in list </returns>
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

    /// <summary>
    /// Get all the contacts from the contact list, and read contact list
    /// </summary>
    /// <param name="_contactList">a list of type name</param>
    /// <returns>Return list of contacts if its not empty</returns>
    public List<IContact> GetContactsFromList()
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
}
