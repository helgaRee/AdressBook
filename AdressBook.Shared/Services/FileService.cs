// ### LÄSA OCH SKRIVA TILL FIL ###
using System.Diagnostics;

namespace AdressBook.Shared.Services;

public class FileService(string filePath)
{
    private readonly string _filePath = filePath;

    /// <summary>
    /// Save a contact to file
    /// </summary>
    /// <param name="contact">A contact of type string</param>
    /// <returns>Returns true if the contact was successfully saved to file, else returns false</returns>
    public bool SaveContactToFile(string contact)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.Write(contact);
            }
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    /// <summary>
    /// Get a contact from file
    /// </summary>
    /// <param name=""></param>
    /// <returns>If contact exist in file, return contact, else return null.</returns>
    public string GetContactFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                return sr.ReadToEnd();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading file {_filePath}: {ex.Message}");
        }
        return null!;
    }
}