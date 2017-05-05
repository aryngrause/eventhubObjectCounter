using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using System.Diagnostics;
using System.IO;

namespace EventHubCount
{
    public class Program
    {
        static string eventHubName = "[event hubs name]";
        static string connectionString = "[service bus connection string that holds the event hub]";
        static int partitonCount = 16;
        static string outputTestFilePath = "[output file location, i.e. on local machine]";

        static void Main(string[] args)
        {
            EventHubClient client = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            List<string> countResults = new List<string>();
            countResults.Add(eventHubName);

            for (int partitionId = 0; partitionId < partitonCount; partitionId++)
            {
                PartitionRuntimeInformation item = client.GetPartitionRuntimeInformation(partitionId.ToString());
                long count = (item.LastEnqueuedSequenceNumber - (item.BeginSequenceNumber + 1));

                string countString = "Partition: " + partitionId  + ", LastEnqueuedSequenceNumber: " + item.LastEnqueuedSequenceNumber  + ", BeginSequenceNumber: " + item.BeginSequenceNumber + ", Count: " + count;
                countResults.Add(countString);

                WriteOutResults(countResults);
            }            
        }

        static public void WriteOutResults(List<string> countResults)
        {
            File.WriteAllLines(outputTestFilePath, countResults.ToArray());
        }
    }
}
