namespace AdressBook.Shared.Interfaces;

public interface IContactService
{
    //metoder
    bool AddContactToList(IContact contact);
    void UpdateContact(IContact existingContact, IContact updateContact);
    bool DeleteContactFromList(string email);
    List<IContact> GetContactsFromList();
    bool DeleteContactFromList(IContact contact);
}
