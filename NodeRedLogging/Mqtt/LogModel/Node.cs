namespace NodeRedLogging.Mqtt.LogModel
{
    public class Node
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Flow { get; set; }
        public override string ToString()
        {
            var name = string.IsNullOrWhiteSpace(Name) ? Id : Name;
            return $"{name} ({Type})";
        }
    }
}