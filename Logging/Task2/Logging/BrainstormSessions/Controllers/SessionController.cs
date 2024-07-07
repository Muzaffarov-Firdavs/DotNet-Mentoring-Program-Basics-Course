using System;
using System.Threading.Tasks;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILogger<SessionController> _logger;

        public SessionController(IBrainstormSessionRepository sessionRepository, ILogger<SessionController> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            {
                _logger.LogWarning("Index action: Session ID is not provided");
                return RedirectToAction(nameof(Index), "Home");
            }

            try
            {
                var session = await _sessionRepository.GetByIdAsync(id.Value);

                if (session == null)
                {
                    _logger.LogWarning("Session {SessionId} not found", id.Value);
                    return Content("Session not found.");
                }

                var viewModel = new StormSessionViewModel()
                {
                    DateCreated = session.DateCreated,
                    Name = session.Name,
                    Id = session.Id
                };

                _logger.LogInformation("Retrieved session details for Session ID {SessionId}", id.Value);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing Session ID {SessionId}", id.Value);
                throw;
            }
        }
    }
}