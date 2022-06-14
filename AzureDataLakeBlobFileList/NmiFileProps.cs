using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDataLakeBlobFileList
{
    public record NmiFileProps(string Filename, DateTimeOffset? CreatedOn, string Component1, string Component2, string Component3, string Component4, string Component5, string Component6)
    {
        public static string Header()
        {
            return "\"Filename\",\"CreatedOn\",\"Component1\",\"Component2\",\"Component3\",\"Component4\",\"Component5\",\"Component6\"\n";
        }
        public override string ToString()
        {
            return string.Format("\"{0}\",\"{1}\",\"{2}\",{3},{4},{5},\"{6}\",\"{7}\"\n", this.Filename, this.CreatedOn?.ToString("u"),
                this.Component1, this.Component2, this.Component3, this.Component4, this.Component5, this.Component6);
        }
    }
}
