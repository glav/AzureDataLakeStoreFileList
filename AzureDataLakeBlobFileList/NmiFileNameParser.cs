using Azure.Storage.Files.DataLake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeBlobFileList
{
    internal class NmiFileNameParser
    {
        private string _originalName;

        public NmiFileNameParser(PathItem item, string directoryName)
        {
            _originalName = item.Name.Replace($"{directoryName}/", string.Empty);
            ExtractProps(item);

        }

        public NmiFileProps ExtractProps(PathItem item)
        {
            if (string.IsNullOrWhiteSpace(_originalName) || item == null)
            {
                return new NmiFileProps(_originalName, item?.CreatedOn, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
            var hashComponents = _originalName.Split('#');
            if (hashComponents.Length > 1)
            {
                return ExtractHashSeparatorParts(item, hashComponents);
            }

            var underscoreComponents = _originalName.Split('_');
            if (underscoreComponents.Length > 0)
            {
                return new NmiFileProps(_originalName, item?.CreatedOn, underscoreComponents[0],
                        underscoreComponents.Length > 1 ? underscoreComponents[1] : string.Empty,
                        underscoreComponents.Length > 2 ? underscoreComponents[2] : string.Empty,
                        underscoreComponents.Length > 3 ? underscoreComponents[3] : string.Empty,
                        underscoreComponents.Length > 4 ? underscoreComponents[4] : string.Empty,
                        underscoreComponents.Length > 5 ? underscoreComponents[5] : string.Empty);
            }

            return new NmiFileProps(_originalName, item?.CreatedOn, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }


        private NmiFileProps ExtractHashSeparatorParts(PathItem item,string[] hashComponents)
        {
            if (hashComponents.Length > 2)
            {
                var fileType = hashComponents[0];// cleaned.Substring(0,pos);
                var year = hashComponents[1].Substring(0, 4);
                var month = hashComponents[1].Substring(4, 2);
                var day = hashComponents[1].Substring(6, 2);

                return new NmiFileProps(_originalName, item?.CreatedOn, fileType, year, month, day, hashComponents[2], hashComponents[3]);
            }
            else
            {
                return new NmiFileProps(_originalName, item?.CreatedOn, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }

        }
    }


}
