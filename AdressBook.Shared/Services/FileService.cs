// ### LÄSA OCH SKRIVA TILL FIL ###

using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdressBook.Shared.Services;

public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;



    public bool SaveContactToFile(string contact)
    {
        try
        {

            using (var sw = new StreamWriter(_filePath))
            {
                sw.Write(contact); //skriver in content i filen
            }
            return true;
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }




    public string GetContactFromFile()
    {
        try
        {
            //kontroll om filen existerar med sw, om true = using
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
