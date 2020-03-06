using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NodeRedLogging.Mqtt.LogModel
{
    public interface IData
    {
        Dictionary<string, JToken> Other { get; set; }
    }
}