using AdressBook.Shared.Interfaces;

namespace AdressBook.Shared.Models;

public class Contact : IContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
   
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public Contact(string firstName, string lastName, string email, string address, string city, string postalCode, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        City = city;
        PostalCode = postalCode;
        PhoneNumber = phoneNumber;
    }

    public Contact()
    {
    }
}
