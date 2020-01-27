using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodeRedLogging.Logging;

namespace NodeRedLogging
{
    public class LoggingModel : PageModel
    {
        private readonly InMemoryLogger _inMemoryLogger;

        public List<LogModel> Logs { get; private set; }

        public LoggingModel(InMemoryLogger inMemoryLogger)
        {
            _inMemoryLogger = inMemoryLogger;
        }

        public void OnGet()
        {
            Logs = _inMemoryLogger.GetAll().Reverse().ToList();
        }
    }
}