using System;

namespace BrainstormSessions.ViewModels
{
    public class IdeaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
