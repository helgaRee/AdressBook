// ### UTFÖR ALLA HANDLINGAR ###
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using AdressBook.Shared.Repositories;

namespace AdressBook.Shared.Repositories;

public class ContactRepository
{
    private List<Contact> _contactList = [];
    //  private readonly ContactService _contactService;

    public void AddToList(Contact contact)
    {
        _contactList.Add(contact);
    }

    public void DeleteFromList(Contact contact)
    {
        _contactList.Remove(contact);
    }

    public IEnumerable<Contact> GetAllFromList()
    {
        return _contactList;
    }
}
