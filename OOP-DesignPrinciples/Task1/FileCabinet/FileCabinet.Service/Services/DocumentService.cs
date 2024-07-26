using FileCabinet.Domain.Models;
using FileCabinet.Service.Interfaces;
using System.Text.Json;

namespace FileCabinet.Service.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly string _storagePath;

        public DocumentService(string storagePath)
        {
            _storagePath = storagePath;
        }

        public Document? SearchDocumentByNumber(int documentNumber)
        {
            var files = Directory.GetFiles(_storagePath, "*_#" + documentNumber + ".json");

            if (files.Length == 0) return null;

            var file = files[0];
            var json = File.ReadAllText(file);
            var documentType = Path.GetFileNameWithoutExtension(file).Split('_')[0];

            return documentType switch
            {
                "book" => JsonSerializer.Deserialize<Book>(json),
                "localizedbook" => JsonSerializer.Deserialize<LocalizedBook>(json),
                "patent" => JsonSerializer.Deserialize<Patent>(json),
                _ => null,
            };
        }
    }
}
