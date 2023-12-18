﻿namespace AdressBook.Shared.Interfaces;

public interface IContactService
{
    bool AddContactToList(IContact contact);
    void UpdateContact(IContact existingContact, IContact updateContact);
    bool DeleteContactFromList(string email);
    List<IContact> GetContactsFromList();
}
