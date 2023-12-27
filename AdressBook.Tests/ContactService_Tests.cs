using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using Newtonsoft.Json;
using System.Diagnostics;



namespace AdressBook.Tests;

public class ContactService_Tests
{
    [Fact]
    public void AddContactToListShould_AddOneContactToContactList_ThenReturnTrue()
    {
        //Arrange
        IContact contact = new Contact("John", "Doe", "john.doe@example.com", "123 Main St", "Cityville", "12345", "555-1234");
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
        IContact contact = new Contact("John", "Doe", "john.doe@example.com", "123 Main St", "Cityville", "12345", "555-1234");
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
        IContact contact = new Contact("John", "Doe", "john.doe@example.com", "123 Main St", "Cityville", "12345", "555-1234");
        IContactService contactService = new ContactService();
        contactService.AddContactToList(contact);

        //Act
        List<IContact> result = contactService.GetContactsFromList();


        //Assert - förväntas inte bli null och att listan innehåller någonting (assert är olika kontroller som görs)
        Assert.NotNull(result);

    }
}
