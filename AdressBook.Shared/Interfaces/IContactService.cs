

namespace AdressBook.Shared.Interfaces;

public interface IContactService
{
    //metoder
    bool AddContactToList(IContact contact);
    bool DeleteContactFromList(string email);
    bool DeleteContactFromList(IContact contact);
    List<IContact> GetContactsFromList();


    IEnumerable<IContact> GetAllFromList();

}
