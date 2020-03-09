using System;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NodeRedLogging.Mqtt.LogModel;

namespace NodeRedLogging.Mqtt
{
    public class MqttClient : IDisposable
    {
        private readonly ILogger<MqttClient> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private IMqttClient _mqttClient;
        private const string Host = "192.168.10.58";
        private const string Topic = "nodered/log";

        public MqttClient(ILogger<MqttClient> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public async Task Initialize()
        {
            try
            {
                _mqttClient = await System.Net.Mqtt.MqttClient.CreateAsync(Host);
                _logger.LogInformation($"Connect to {Host}");
                await _mqttClient.ConnectAsync();
                _logger.LogInformation($"Connected to {Host}");
                await _mqttClient.SubscribeAsync(Topic, MqttQualityOfService.ExactlyOnce);
                _logger.LogInformation($"Subscribed to {Topic}");
                _mqttClient.MessageStream.Subscribe(OnMessage);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
            }
        }

        private void OnMessage(MqttApplicationMessage msg)
        {
            var logMessage = Encoding.UTF8.GetString(msg.Payload);
            if(!ParseAndLog(logMessage)) _logger.LogWarning($"RAW: {logMessage}");
        }

        private bool ParseAndLog(string jsonMessage)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<Message>(jsonMessage);
                if (message.Node == null || string.IsNullOrWhiteSpace(message.Node.Flow))
                {
                    _logger.LogWarning("No log info");
                    return false;
                }

                var logger = _loggerFactory.CreateLogger($"{message.Node.Flow}.{message.Node.NameOrType}");
                var level = GetLogLevel(string.Empty);
                logger.Log(level, message.ToString());
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Failed to parse: '{ex.Message}'");
                return false;
            }
        }

        private LogLevel GetLogLevel(string level)
        {
            return Enum.TryParse(level, out LogLevel logLevel)
                ? logLevel
                : LogLevel.Information;
        }

        public void Dispose()
        {
            _mqttClient?.Dispose();
        }
    }
}