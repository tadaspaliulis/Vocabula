using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.Storage.DataFormats
{
    /// <summary>
    /// Base type representing a file used by serialisers.
    /// </summary>
    [Serializable]
    public abstract class BaseDataStorageFileFormat
    {
        /// <summary>
        /// File version.
        /// </summary>
        public DataFileVersionNumber VersionNumber;
    }
}
