using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeBlobFileList
{
    internal record ConfigItems(string BlobConnectionString, string[] directoriesToEnumerate);
}
