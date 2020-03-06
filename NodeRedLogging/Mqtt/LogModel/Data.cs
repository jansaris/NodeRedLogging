using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NodeRedLogging.Mqtt.LogModel
{
    [JsonConverter(typeof(DataConverter<Data>))]
    public class Data : IData
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
        public Dictionary<string, JToken> Other { get; set; } = new Dictionary<string, JToken>();

        public override string ToString()
        {
            return $"Topic: {Topic}, Payload: {Payload}, Other: {SerializeOther()}";
        }

        private string SerializeOther()
        {
            if (Other == null || Other.Count == 0) return string.Empty;
            try
            {
                var builder = new StringBuilder();
                builder.Append("{");
                foreach (var key in Other.Keys)
                {
                    builder.Append("'");
                    builder.Append(key);
                    builder.Append("':");
                    builder.Append(Other[key]);
                }
                builder.Append("}");
                return builder.ToString();
            }
            catch
            {
                return "Failed to serialize other properties";
            }
        }
    }
}