using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.Storage.DataFormats.Version1
{
    /// <summary>
    /// Represents XML structure of the statistics of a single word
    /// </summary>
    [Serializable]
    public class WordStatV1
    {
        public DateTime DateLearned;
        public DateTime LastTimeAnswered;
        public int NumberOfTimesAsked;
        public int NumberOfTimesAnsweredCorrectly;
        public string UniqueId;
    }

    /// <summary>
    /// Represents the stats file XML structure
    /// </summary>
    [Serializable]
    public class WordStatsListV1 : BaseDataStorageFileFormat
    {
        public List<WordStatV1> WordStats;
    }
}
