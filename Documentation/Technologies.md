# RabbitMQ

### What is RabbitMQ?
* A Message Broker - it accepts and forwards messages
* Messages are sent by Producers (or Publishers)
* Messages are received by Consumers (or Subscribers)
* Messages are stored on Queues (essentially a message buffer)
* Exchanges can be used to add "routing" functionality
* Uses Advanced Message Queuing Protocol (AMQP) & others

#### 4 Types of Exchange 
* Direct Exchange 
  * Delivers Messages to queues based on a routing key 
  * Ideal for "direct" or unicast messaging
* Fanout Exchange 
  * Delivers Messages to all queues that are bound to the exchange 
  * It ignores the routing key 
  * Ideal for broadcast messages
* Topic Exchange 
  * Routes messages to 1 or more queus based on the routing key and patterns
  * Used for Multicast messaging
  * Implements various Publisher/Subscriber Patterns
* Header Exchange 

# gRPC

### What is gRPC?
* "Google" Remote Procedure Call 
* Uses HTTP/2 protocol to transport binary messages (inc. TLS)
* Focused on high performance 
* Relies on "Protocol Buffers" aka Protobuf to defined the contract between end points 
* Mutli-language support (C# client can call a Ruby service)
* Frequently used as a method of service to service communication