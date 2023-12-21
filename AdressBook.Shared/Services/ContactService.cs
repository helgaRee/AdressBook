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
    private List<IContact> _contactList = new List<IContact>();


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
        catch (Exception ex) { Debug.WriteLine(ex); }

    }


    //update kontakt
    public void UpdateContact(Contact selectedContact)
    {
        try
        {
            //Hitta indexet för den befintliga kontakten i listan
            int index = _contactList.FindIndex(current => current.Email == selectedContact.Email);

            if (index != -1)
            {
                //uppdatera kontakt i listan
                _contactList[index] = selectedContact;
                //Spara den uppdaterade listan till filen
                _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
            }
            else
            {
                //om kontakten inte finns i listan, lägg till
                _contactList.Add(selectedContact);
                //spara den uppdaterade listan till filen
                _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
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
            var contactList = GetContactsFromList();

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

    //Get and read contactList
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


}