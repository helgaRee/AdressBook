using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdressBook.Shared.Services;

public interface IFileService
{
    bool SaveContactToFile(string contact);

    string GetContactFromFile();
}

//sätter ett privat fält och initierar den i konstuktorn, vilket gör att
//första gången jag gör en instans av min fileService, sätts en filePath = färdighanterat!
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
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


    
}
