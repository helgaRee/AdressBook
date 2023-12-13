using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using AdressBook.Shared.Services;
using Xunit;

namespace AdressBook.Tests;
//Public för att konna dela filer mellan projekt
public class ContactService_Tests
{
    //fact - vårat test - lägga till en kontakt till listan
    [Fact]
    public void AddContactToList_AddOneContactToContactList_ThenReturnTrue()
    {
        //Arrange - vilka förberedelser behövs göras?
        IContact contact = new Contact 
        (
            "Helga", 
            "Reesalu", 
            "helga@domain.com", 
            "Helgeandsgatan", 
            "Lund", 
            "1234", 
            "123123123" 
        );
        //instansiering
        IContactService contactService = new ContactService();

        //Act - lägg till kontakten i adressboken - metoden genereras i interfacet
        bool result = contactService.AddContactToList( contact );

        //Assert - kontrollera om den finns i listan - ska bli true
        Assert.True( result );
    }

    [Fact]
    public void DeleteContactFromListShould_DeleteOneContactFromList_ThenReturnTrue()
    {
        //Arrange
        //instansiering
        IContactService contactService = new ContactService();
        IContact contact = new Contact
        (
            "Helga",
            "Reesalu",
            "helga@domain.com",
            "Helgeandsgatan",
            "Lund",
            "1234",
            "123123123"
        );
        
        //Act - Lägg till kontakten
        contactService.AddContactToList(contact);

        //Act - Ta bort kontakten
        bool deleteResult = contactService.DeleteContactFromList(contact.Email);

        //Assert
        Assert.True( deleteResult );

        // Kontrollera om kontakten inte längre finns i listan
        var contactsAfterDelete = contactService.GetAllFromList();
        Assert.DoesNotContain(contactsAfterDelete, currentC => currentC.Email == contact.Email);
    }

    [Fact]
    public void GetContactsFromListShould_GetAllContactsFromList_ThenReturnList()
    {
        //Arrange
        IContactService contactService = new ContactService();
        IContact contact = new Contact
          (
              "Helga",
              "Reesalu",
              "helga@domain.com",
              "Helgeandsgatan",
              "Lund",
              "1234",
              "123123123"
          );
            contactService.AddContactToList(contact);

        //Act
        IEnumerable<IContact> result = contactService.GetAllFromList();

        //Assert - förväntas inte bli null och att listan innehåller någonting (assert är olika kontroller som görs)
        Assert.NotNull( result );
        Assert.True(result.Any()); //innehåller den något värde?

       
    }

}
