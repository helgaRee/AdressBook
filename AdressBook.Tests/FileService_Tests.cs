using AdressBook.Shared.Interfaces;
using AdressBook.Shared.Services;

namespace AdressBook.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveToFileShould_SaveContactToFile_ThenReturnTrue()
    {
        //Arrange
        IFileService fileService = new FileService();
        string filePath = @"c:\Projects\test.txt";
        string content = "Test content";
        //Act
        bool result = fileService.SaveContactToFile(filePath, content);
        //Assert
        Assert.True(result);

    }
}
