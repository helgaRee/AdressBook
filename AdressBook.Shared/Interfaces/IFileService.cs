// ### LÄSA OCH SKRIVA TILL FIL ###

namespace AdressBook.Shared.Interfaces;

public interface IFileService
{
    bool SaveContactToFile(string contact);

    string GetContactFromFile();
}
