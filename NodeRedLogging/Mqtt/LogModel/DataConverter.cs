using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NodeRedLogging.Mqtt.LogModel
{
    public class DataConverter<T> : JsonConverter<T> 
        where T : IData, new()
    {
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        { 
            var data = new T();
            var props = objectType.GetTypeInfo().DeclaredProperties.ToList();

            var jo = JObject.Load(reader);
            foreach (var jp in jo.Properties())
            {
                var prop = props.FirstOrDefault(pi => pi.CanWrite && pi.Name.Equals(jp.Name, StringComparison.InvariantCultureIgnoreCase));

                if (prop != null) prop.SetValue(data, jp.Value.ToObject(prop.PropertyType, serializer));
                else
                {
                    if (data.Other.ContainsKey(jp.Name)) data.Other[jp.Name] = jp.Value;
                    else data.Other.Add(jp.Name, jp.Value);
                }
            }

            return data;
        }
    }
}
