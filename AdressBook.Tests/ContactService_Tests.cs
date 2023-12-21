﻿using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using Xunit;

namespace AdressBook.Tests;

public class ContactService_Tests
{
    [Fact]
    public void AddContactToListShould_AddOneContactToContactList_ThenReturnTrue()
    {
        //Arrange
        IContact contact = new Contact { FirstName = "Helga", LastName = "Reesalu", Email = "helga@domain.com", Address = "Helgeandsgatan", City = "Lund", PostalCode = "1234", PhoneNumber = "123123123" };
        IContactService contactService = new ContactService();

        //Act - lägg till kontakten i adressboken - metoden genereras i interfacet
        bool result = contactService.AddContactToList(contact);

        //Assert - kontrollera om den finns i listan - ska bli true
        Assert.True(result);
    }

    [Fact]
    public void DeleteContactFromListShould_DeleteOneContactFromList_ThenReturnTrue()
    {
        //Arrange
        IContact contact = new Contact { FirstName = "Helga", LastName = "Reesalu", Email = "helga@domain.com", Address = "Helgeandsgatan", City = "Lund", PostalCode = "1234", PhoneNumber = "123123123" };
        IContactService contactService = new ContactService();

        //Act
        contactService.AddContactToList(contact);
        bool deleteResult = contactService.DeleteContactFromList(contact.Email);


        //Assert
        Assert.True(deleteResult);

        //Kontroll
        var contactsAfterDelete = contactService.GetContactsFromList();
        Debug.WriteLine("Kontakter efter borttagning: " + JsonConvert.SerializeObject(contactsAfterDelete));
        Assert.DoesNotContain(contactsAfterDelete, currentC => currentC.Email == contact.Email);

    }



    [Fact]
    public void GetAllFromListShould_GetAllContactsFromList_ThenReturnListOfContacts()
    {
        //Arrange
        //Arrange
        IContact contact = new Contact { FirstName = "Helga", LastName = "Reesalu", Email = "helga@domain.com", Address = "Helgeandsgatan", City = "Lund", PostalCode = "1234", PhoneNumber = "123123123" };
        IContactService contactService = new ContactService();
        contactService.AddContactToList(contact);

        //Act
        List<IContact> result = contactService.GetContactsFromList();


        //Assert - förväntas inte bli null och att listan innehåller någonting (assert är olika kontroller som görs)
        Assert.NotNull(result);

    }
}
