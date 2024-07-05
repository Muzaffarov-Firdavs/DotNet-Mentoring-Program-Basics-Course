using System.Collections.Generic;
using System.Threading.Tasks;
using BrainstormSessions.Core.Model;

namespace BrainstormSessions.Core.Interfaces
{
    public interface IBrainstormSessionRepository
    {
        Task<BrainstormSession> GetByIdAsync(int id);
        Task<List<BrainstormSession>> ListAsync();
        Task AddAsync(BrainstormSession session);
        Task UpdateAsync(BrainstormSession session);
    }
}
