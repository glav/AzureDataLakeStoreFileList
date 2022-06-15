// See https://aka.ms/new-console-template for more information

using Azure.Storage.Files.DataLake;
using AzureDataLakeBlobFileList;
using System.Text;

var configItems = Startup.Configure();

Console.WriteLine($"...Accessing container [{configItems.ContainerName}]");
var client = new DataLakeFileSystemClient(configItems.BlobConnectionString, configItems.ContainerName);

Console.WriteLine($"...Getting folders in [{configItems.ContainerName}]");
var paths = client.GetPaths();

Console.WriteLine($"...Found [{paths.Count()}] folders\n");
paths.ToList().ForEach(p => Console.WriteLine($"- [{p.Name}]"));

Console.WriteLine();

foreach (var dirName in configItems.directoriesToEnumerate)
{
    Console.WriteLine($"...Accessing '{dirName}'");
    var dirClient = client.GetDirectoryClient(dirName);
    var pathsInDir = dirClient.GetPaths();

    var pathsEnumerator = dirClient.GetPaths().GetEnumerator();
    pathsEnumerator.MoveNext();
    var item = pathsEnumerator.Current;

    var cnt = 0;
    long totalCount = 0;

    var buffer = new StringBuilder();

    buffer.AppendFormat(NmiFileProps.Header());

    while (item != null)
    {
        if (item != null)
        {
            var parser = new NmiFileNameParser(item, dirName);
            var props = parser.ExtractProps(item);
            buffer.AppendFormat(props.ToString());
        }

        cnt++;
        totalCount++;

        if (!pathsEnumerator.MoveNext()) break;
        if (cnt > 50)
        {
            Console.Write(".");
            cnt = 0;
        }
        item = pathsEnumerator.Current;
    }

    var exportFilename = $"Bloblist-{dirName}.csv";
    Console.WriteLine($"\n...Writing data to {exportFilename}");
    File.WriteAllText($"./{exportFilename}", buffer.ToString());

    Console.WriteLine($"...Found [{totalCount}] files in '{dirName}'\n");
}
Console.WriteLine("Done");
Console.ReadLine();

