const mqtt = require('mqtt')
const client = mqtt.connect('mqtt://192.168.10.58')

var topic = 'nodered/log';

client.on('connect', () => {
  client.subscribe(topic);
  console.log('subscribed to %s', topic);
  
    client.publish(topic, 'Hello');
    console.log('send hello');
    client.publish(topic, 'world');
    console.log('send world');
});

client.on('message', (topic, message) => {
  console.log("Got '%s' on topic %s", message, topic)
});

console.log('registered function on message');

return 0;