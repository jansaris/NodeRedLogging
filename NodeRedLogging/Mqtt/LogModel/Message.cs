namespace NodeRedLogging.Mqtt.LogModel
{
    public class Message
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public Node Node { get; set; }
        public Data Data { get; set; }

        public override string ToString()
        {
            return $"{Id} {Node}.{Action}: {Data}";
        }
    }
}