using FileCabinet.Domain.Models;

namespace FileCabinet.Service.Interfaces
{
    public interface IDocumentService
    {
        Document? SearchDocumentByNumber(int documentNumber);
    }
}
