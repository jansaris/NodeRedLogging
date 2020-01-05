namespace NodeRedLogging.Mqtt
{
    public class Message
    {
        public string Payload { get; set; }
        public string Data { get; set; }
        public string Topic { get; set; }
        public LogDetails LogInfo { get; set; }
    }
}