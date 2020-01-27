logging: {
    console: {
        level: "info",
        metrics: false,
        audit: false
    },
    mqtt: {
        level:'info',
        metrics:true,
        handler: function(conf) {
            var host = 'mqtt://192.168.10.58';
            var topic = 'nodered/log';
            
            var mqtt = require('mqtt');
            var client = mqtt.connect(host);
            var connected = false;

            client
                .on('connect', function() {
                    console.log("Mqtt logger connected");
                    connected = true;
                })
                .on('error', function(err) {
                    console.log("Mqtt logger error: " + err);
                    connected = false;
                });
                
            // Return the function that will do the actual logging
            return function(msg) {
                if (!connected) {
                    console.log('No connection to: %s, ignore message', host);
                    return;
                }
                var message = {
                    'tags': ['node-red', 'mqtt', 'logs'],
                    'fields': msg,
                    'timestamp': (new Date(msg.timestamp)).toISOString()
                };
                try {
                    client.publish(topic, message);
                } catch(err) { 
                    console.log(err);
                }
            }
        }
    }
}