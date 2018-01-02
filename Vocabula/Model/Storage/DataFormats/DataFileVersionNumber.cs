using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.Storage.DataFormats
{
    /// <summary>
    /// File version as stored in the data file, should inherit from IComparable once several file types are supported
    /// </summary>
    [Serializable]
    public class DataFileVersionNumber
    {
        public DataFileVersionNumber()
        {

        }

        public DataFileVersionNumber(int version, int major, int minor)
        {
            Version = version;
            Major = major;
            Minor = minor;
        }

        public int Version;
        public int Major;
        public int Minor;
    }
}
