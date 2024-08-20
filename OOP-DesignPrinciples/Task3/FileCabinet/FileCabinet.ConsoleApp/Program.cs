using FileCabinet.Domain.Models;
using FileCabinet.Service.Interfaces;
using FileCabinet.Service.Services;

class Program
{
    static void Main(string[] args)
    {
        string storagePath = @"C:\Users\Firdavs_Muzaffarov\Desktop\DotNet-Mentoring-Program-Basics-Course\OOP-DesignPrinciples\Task3\FileCabinet\FileCabinet.Data\JsonFiles\";

        var cacheService = new CacheService();
        var cacheDurations = new Dictionary<string, TimeSpan>
        {
            { "book", TimeSpan.FromMinutes(10) },
            { "localizedbook", TimeSpan.FromMinutes(5) },
            { "patent", TimeSpan.MaxValue }, 
            { "magazine", TimeSpan.Zero }
        };
        IDocumentService documentService = new DocumentService(storagePath, cacheService, cacheDurations);

        Console.WriteLine("Enter the document number to search:");
        if (int.TryParse(Console.ReadLine(), out int documentNumber))
        {
            var document = documentService.SearchDocumentByNumber(documentNumber);
            if (document != null)
            {
                PrintDocumentInfo(document);
            }
            else
            {
                Console.WriteLine("Document not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid document number.");
        }
    }

    static void PrintDocumentInfo(Document document)
    {
        switch (document)
        {
            case LocalizedBook localizedBook:
                Console.WriteLine($"Localized Book: {localizedBook.Title}, ISBN: {localizedBook.ISBN}, Authors: {string.Join(", ", localizedBook.Authors)}, Pages: {localizedBook.NumberOfPages}, Original Publisher: {localizedBook.OriginalPublisher}, Country of Localization: {localizedBook.CountryOfLocalization}, Local Publisher: {localizedBook.LocalPublisher}, Date Published: {localizedBook.DatePublished}");
                break;
            case Book book:
                Console.WriteLine($"Book: {book.Title}, ISBN: {book.ISBN}, Authors: {string.Join(", ", book.Authors)}, Pages: {book.NumberOfPages}, Publisher: {book.Publisher}, Date Published: {book.DatePublished}");
                break;
            case Patent patent:
                Console.WriteLine($"Patent: {patent.Title}, Authors: {string.Join(", ", patent.Authors)}, Date Published: {patent.DatePublished}, Expiration Date: {patent.ExpirationDate}, Unique ID: {patent.UniqueId}");
                break;
            case Magazine magazine:
                Console.WriteLine($"Magazine: {magazine.Title}, Publisher: {magazine.Publisher}, Release Number: {magazine.ReleaseNumber}, Publish Date: {magazine.DatePublished}");
                break;
        }
    }
}
