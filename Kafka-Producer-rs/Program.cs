// See https://aka.ms/new-console-template for more information
using Kafka_Producer_rs;

Console.WriteLine("Hello, World! Producer");
ProduceMessage produceMessage = new ProduceMessage();
produceMessage.CreateMessage().Wait();