using System;

namespace NodeRedLogging.Logging
{
    public class LogModel
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string Flow { get; set; }
        public string Node { get; set; }
    }
}