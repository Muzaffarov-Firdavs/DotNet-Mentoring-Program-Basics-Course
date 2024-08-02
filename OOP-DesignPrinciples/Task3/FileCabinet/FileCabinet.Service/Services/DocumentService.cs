using FileCabinet.Domain.Models;
using FileCabinet.Service.Interfaces;
using System.Text.Json;

namespace FileCabinet.Service.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly string _storagePath;
        private readonly CacheService _cacheService;
        private readonly Dictionary<string, TimeSpan> _cacheDurations;

        public DocumentService(string storagePath, CacheService cacheService, Dictionary<string, TimeSpan> cacheDurations)
        {
            _storagePath = storagePath;
            _cacheService = cacheService;
            _cacheDurations = cacheDurations;
        }

        public Document? SearchDocumentByNumber(int documentNumber)
        {
            var cachedDocument = _cacheService.Get(documentNumber);
            if (cachedDocument != null)
            {
                return cachedDocument.Document;
            }

            var files = Directory.GetFiles(_storagePath, "*_#" + documentNumber + ".json");

            if (files.Length == 0) return null;

            var file = files[0];
            var json = File.ReadAllText(file);
            var documentType = Path.GetFileNameWithoutExtension(file).Split('_')[0];

            Document? document = documentType switch
            {
                "book" => JsonSerializer.Deserialize<Book>(json),
                "localizedbook" => JsonSerializer.Deserialize<LocalizedBook>(json),
                "patent" => JsonSerializer.Deserialize<Patent>(json),
                "magazine" => JsonSerializer.Deserialize<Magazine>(json),
                _ => null,
            };

            if (document != null && _cacheDurations.TryGetValue(documentType, out var cacheDuration))
            {
                _cacheService.Add(documentNumber, document, cacheDuration);
            }

            return document;
        }
    }
}
