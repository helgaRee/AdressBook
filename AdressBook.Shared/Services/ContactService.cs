using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdressBook.Shared.Services;
/// <summary>
/// Lets the user add, delete, get and update contacts.
/// </summary>
public class ContactService : IContactService
{
    private readonly FileService _fileService = new FileService(@"C:\EC\Projects\content.json");
    private List<Contact> _contactList = new List<Contact>();

    /// <summary>
    /// Ádd a new contact to list
    /// </summary>
    /// <param name="contact">A contact of type string</param>
    /// <returns>If the contact is not in list, add to list and save to file.</returns>
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

            return false; // Flytta return false utanför if-blocket
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    /// <summary>
    /// Update a contact in List
    /// </summary>
    /// <param name="selectedContact">The selected contact from user, of type string</param>
    /// <returns>Updates contact if its in list</returns>
    public void UpdateContact(Contact selectedContact)
    {
        try
        {
            //Find index for the current contact in list
            int index = _contactList.FindIndex(current => current.Email == selectedContact.Email);

            if (index != -1)
            {
                //Update contact
                _contactList[index] = selectedContact;
                //Save the updated contact to file
                _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
            }
            else
            {
                _contactList.Add(selectedContact);
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

    /// <summary>
    /// Get contacts from list
    /// </summary>
    /// <param name=""></param>
    /// <returns>Get all the contacts from list as Json</returns>
    public List<Contact> GetContactsFromList()
    {
        try
        {
            var contact = _fileService.GetContactFromFile();
            if (!string.IsNullOrEmpty(contact))
            {

                _contactList = JsonConvert.DeserializeObject<List<Contact>>(contact)!;
                
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); };
        return _contactList;
    }

    public void UpdateContact(IContact existingContact, IContact updateContact)
    {
        throw new NotImplementedException();
    }

    List<IContact> IContactService.GetContactsFromList()
    {
        throw new NotImplementedException();
    }

    public bool DeleteContactFromList(IContact contact)
    {
        throw new NotImplementedException();
    }
}