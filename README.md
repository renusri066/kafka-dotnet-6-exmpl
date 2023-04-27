# kafka-net-6-example
In today's repo, we saw how to implement Kafka with dotnet 6. If you wish to download the code
# What is Kafka?
Kafka is a distributed streaming platform developed by the Apache Software Foundation. It is designed to handle high-volume, real-time data streams and is commonly used for building data pipelines, stream processing applications, and real-time analytics.

At its core, Kafka is a publish-subscribe messaging system that allows producers to write data to topics and consumers to read data from those topics in real-time. Kafka is highly scalable and fault-tolerant, with the ability to handle large volumes of data and provide high throughput and low latency.

Kafka can be used for various use cases, such as real-time processing of streaming data, event sourcing, and messaging between microservices. It is also commonly used in big data environments for data ingestion, processing, and analytics.

# Why do we need Kafka?
There are several reasons why Kafka is an important tool for handling real-time data streams:

# Scalability: Kafka is designed to scale horizontally, which means it can handle large volumes of data and an increasing number of users by adding more servers to the cluster. This makes it an excellent choice for handling high-velocity data streams in modern data-driven applications.
# Reliability: Kafka is a distributed system providing fault tolerance and high availability. This means that even if one or more servers in the cluster fail, the system can continue to operate without data loss.
Real-time processing: Kafka provides low-latency message delivery, which makes it a good choice for applications that require real-time processing of streaming data. With Kafka, data can be processed and analyzed as it arrives, enabling faster decision-making and more efficient operations.
# Flexibility: Kafka is a flexible platform that can be used for a wide range of use cases, including real-time processing, messaging, and data ingestion. It can be integrated with various other tools and systems, making it a versatile tool for modern data-driven applications.
Overall, Kafka provides a powerful and flexible platform for handling real-time data streams, essential for many modern data-driven applications and use cases.

# What are the Cons of Kafka?
While Kafka provides many benefits, there are also some potential downsides to consider when using this technology:

# Complexity

Kafka is a complex system that can be difficult to set up and maintain, especially for smaller teams or organizations with limited resources. It requires a high level of expertise in distributed systems, messaging, and data processing.

# Overhead

Kafka adds an additional layer of complexity to data processing pipelines, which can increase the overhead and cost of the system. This can be especially true for smaller systems that don't need the scalability and fault tolerance provided by Kafka.

# Learning curve

Because Kafka is a relatively new technology, there can be a steep learning curve for developers unfamiliar with its concepts and APIs. This can require additional training and development time to get up to speed.

# Storage limitations

While Kafka is designed for real-time data processing, it may not be the best choice to store large volumes of data over the long term. In particular, it may not be ideal for use cases where data needs to be queried and analyzed after it has been stored for a long time.

While Kafka provides many benefits, it is important to consider these potential downsides and evaluate whether it is the right choice for your specific use case and organizational needs.

Now that we've learned about Kafka let's start with the fun bit. (Coding)

# Now we will create a new console application in dotnet 6 C#
We will name it as KafkaProducer. This application will be responsible for producing Kafka messages
![image](https://user-images.githubusercontent.com/13117547/234949810-3a599b27-5929-4b00-b24d-d58ae3ee236e.png)

Install the Confluent.Kafka NuGet package using the Package Manager Console in Visual Studio or the dotnet CLI:

After adding the package, we will create a new class, "ProduceMessage.cs" and create a method, "CreateMessage" to write our logic to create and push the Kafka message.

Inside our method, we will configure the Kafka producer. Our bootstrapServer should be where our Kafka is running. In our case, we are running Kafka on our local system.
var config = new ProducerConfig {
    BootstrapServers = "localhost:9092",
        ClientId = "my-app",
        BrokerAddressFamily = BrokerAddressFamily.V4,
};

using
var producer = new ProducerBuilder < Null,
    string > (config).Build();
Console.WriteLine("Please enter the message you want to send");
var input = Console.ReadLine();
var message = new Message < Null,
    string > {
        Value = input
    };
var deliveryReport = await producer.ProduceAsync("my-topic", message);

After this, in the Program.cs, we will create the object of our class and call our CreateMessage method.

ProduceMessage produceMessage = new ProduceMessage();
produceMessage.CreateMessage().Wait();
Our work in the KafkaProducer has been done. Now we will create a "KafkaConsumer" project, another console application, to consume and read the message sent out by the topic mentioned in the above code.

In this newly created project, we will create a new file called ConsumeMessage.cs, and we will create a new method in this file called "ReadMessage".

We will create the object of ConsumerConfig and provide our required configurations.

var config = new ConsumerConfig {
    BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        ClientId = "my-app",
        GroupId = "my-group",
        BrokerAddressFamily = BrokerAddressFamily.V4,
};

Here we have the Kafka URL, which is localhost:9092, as we are running Kafka in our local system. All of the configs should be similar to our producer only.

Next, we will create a consumer object and subscribe to the topic we are publishing.

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("my-topic");

After that, we will start a small infinite while loop to read the message from the consumer object.

try {
    while (true) {
        var consumeResult = consumer.Consume();
        Console.WriteLine($ "Message received from {consumeResult.TopicPartitionOffset}: {
                consumeResult.Message.Value
            }
            ");
        }
    } catch (OperationCanceledException) {
        // The consumer was stopped via cancellation token.
    } finally {
        consumer.Close();
    }
