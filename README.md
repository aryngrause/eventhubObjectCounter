# eventhubObjectCounter

Description: Azure provides no tools to count the number of events/objects that are within an eventhub this made it difficult
to test a streaming application I was in the process of developing. 

This application will connect to an Azure eventhub via the service bus connection string and eventhub name to count
the number of events/objects in each partition of the eventhub. It then prints the events out into a text file.
